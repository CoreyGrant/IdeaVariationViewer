using EuObjParser.Attributes;
using EuObjParser.Models.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EuObjParser.Parsing
{
	class EuObjParser
	{
		private const char Sep = '=';
		private const char LeftBracket = '{';
		private const char RightBracket = '}';
		private readonly char[] NameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_ ".ToCharArray();
		private readonly char[] ValueChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/_\" .-".ToCharArray();
		private readonly Regex NameRegex = new Regex("[a-zA-Z0-9_]+");
		private readonly Regex ValueRegex = new Regex("(?:\"[a-zA-Z0-9_ ]+\")|(?:[a-zA-Z0-9_.\\-]+)");

		public List<T> DeserialiseEuObj<T>(StreamReader sr) where T : new()
		{
			var nodes = ParseEuObj(sr).Value;
			return ProcessNodeList<T>(nodes, 0);
		}

		private T ProcessNode<T>(ParentNode node, int index) where T : new()
		{
			return (T)ProcessNode(typeof(T), node, index);
		}

		private object ProcessNode(Type objType, Node node, int index)
		{
			var typeName = objType.FullName;
			var constructors = objType.GetConstructors();
			var objConstructor = objType.GetConstructor(new Type[0]);
			var obj = objConstructor.Invoke(new object[0]);
			var properties = objType.GetProperties();
			
			var namedProps = properties
				.Where(x => x.GetCustomAttributes(typeof(EuPropNameAttribute), false).Any());
			var namedPropNames = namedProps
				.Select(x => ((EuPropNameAttribute)x.GetCustomAttributes(typeof(EuPropNameAttribute), false).Single()).Name);
			
			var nameRemainingProp = properties
				.SingleOrDefault(x => x.GetCustomAttributes(typeof(EuPropNameRemainingAttribute), false).Any());
			
			var remainingPropNameExcept = nameRemainingProp == null
				? new string[0]
				: ((EuPropNameRemainingAttribute)nameRemainingProp.GetCustomAttributes(typeof(EuPropNameRemainingAttribute), false).Single()).Except;
			
			var objNameProp = properties
				.SingleOrDefault(x => x.GetCustomAttributes(typeof(EuObjPropNameAttribute), false).Any());

			var indexProp = properties
				.SingleOrDefault(x => x.GetCustomAttributes(typeof(EuIndexAttribute), false).Any());

			var objValueProp = properties
				.SingleOrDefault(x => x.GetCustomAttributes(typeof(EuObjPropValueAttribute)).Any());

			var listProps = properties
				.Where(x =>
					x.GetCustomAttributes(typeof(EuObjListAttribute)).Any());

			if (objNameProp != null)
			{
				SetValue(objNameProp, obj, node.Key);
			}
			if (indexProp != null)
			{
				SetValue(indexProp, obj, index.ToString());
			}
			if (node is ValueNode vn)
			{
				if (objValueProp != null)
				{
					SetValue(objValueProp, obj, vn.Value ?? vn.Key);
				}
				return obj;
			}
			var parentNode = node as ParentNode;
			if (objValueProp != null)
			{
				if(objValueProp.PropertyType == typeof(string))
				{
					SetValue(objValueProp, obj, ((ValueNode)parentNode.Value.Single()).Key, false);
				} else 
				if (typeof(IEnumerable).IsAssignableFrom(objValueProp.PropertyType))
				{
					objValueProp.SetValue(obj, ProcessNodeList(objValueProp.PropertyType, parentNode.Value, index));
				}
			}
			var localIndex = 0;
			if (listProps.Any())
			{
				foreach(var prop in listProps)
				{
					var listAtt = (EuObjListAttribute)prop.GetCustomAttributes(typeof(EuObjListAttribute)).Single();
					if (prop.GetCustomAttributes(typeof(EuObjCollapseAttribute)).Any())
					{
						var collapseAtt = (EuObjCollapseAttribute)prop.GetCustomAttribute(typeof(EuObjCollapseAttribute));
						var nodeCursor = parentNode;
						foreach(var propName in collapseAtt.PropNames)
						{
							nodeCursor = (ParentNode)nodeCursor.Value.SingleOrDefault(x => x.Key == propName);
							if(nodeCursor == null)
							{
								break;
							}
						}
						if(nodeCursor != null)
						{
							var matchingNodes = nodeCursor.Value.Where(x => x.Key == listAtt.Name).ToList();
							prop.SetValue(obj, ProcessNodeList(prop.PropertyType, matchingNodes, localIndex++));
						}
					}
					else
					{
						var matchingNodes = parentNode.Value.Where(x => x.Key == listAtt.Name).ToList();
						prop.SetValue(obj, ProcessNodeList(prop.PropertyType, matchingNodes, localIndex++));
					}
					
				}
			}
			if (nameRemainingProp != null)
			{
				var attr = (EuPropNameRemainingAttribute)nameRemainingProp.GetCustomAttributes(typeof(EuPropNameRemainingAttribute), false).Single();
				var namesToIgnore = namedPropNames.Concat(attr.Except);
				var nodes = parentNode.Value.Where(x => !namesToIgnore.Contains(x.Key));
				nameRemainingProp.SetValue(obj, ProcessNodeList(nameRemainingProp.PropertyType, nodes.ToList(), localIndex++));
			}

			foreach (var subNode in parentNode.Value)
			{
				PropertyInfo prop = null;
				if (namedPropNames.Contains(subNode.Key))
				{
					prop = namedProps
						.Single(x => ((EuPropNameAttribute)x.GetCustomAttributes(typeof(EuPropNameAttribute), false).Single()).Name == subNode.Key);
					if (subNode is ValueNode sn)
					{
						SetValue(prop, obj, sn.Value);
					}
					else if (subNode is ParentNode pn)
					{
						if (prop.GetCustomAttributes(typeof(EuObjCollapseAttribute)).Any())
						{
							var collapseAtt = (EuObjCollapseAttribute)prop.GetCustomAttributes(typeof(EuObjCollapseAttribute)).Single();
							var collapsedNode = pn;
							foreach (var propName in collapseAtt.PropNames) {
								collapsedNode = (ParentNode)collapsedNode.Value.SingleOrDefault(x => x.Key == propName);
								if(collapsedNode == null)
								{
									break;
								}
							}
							if(collapsedNode != null)
							{
								pn = collapsedNode;
							}
						}
						if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
						{
							prop.SetValue(obj, ProcessNodeList(prop.PropertyType, pn.Value, localIndex++));
						}
						else
						{
							prop.SetValue(obj, ProcessNode(prop.PropertyType, pn, localIndex++));
						}
					}
				}
			}
			return obj;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="objType">The type of the List</param>
		/// <param name="node"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private object ProcessNodeList(Type objType, List<Node> nodes, int index)
		{
			var listGenericType = objType.GetGenericArguments()[0];
			var list = objType.GetConstructor(new Type[0]).Invoke(new object[0]);
			var addToList = objType.GetMethods().Single(x => x.Name == "Add");
			var localIndex = 0;
			foreach(var node in nodes)
			{
				if ((listGenericType.IsValueType || listGenericType == typeof(string)))
				{
					if (node is ValueNode vn)
					{
						var value = GetValue(listGenericType, vn.Value);
						addToList.Invoke(list, new[] { value });
					}
					else if(node is ParentNode pn)
					{
						var valueNodes = pn.Value;
						foreach(ValueNode n in valueNodes)
						{
							var value = GetValue(listGenericType, n.Value ?? n.Key);
							addToList.Invoke(list, new[] { value });
						}
					}
				}
				else
				{
					var processedNode = ProcessNode(listGenericType, node, localIndex++);
					addToList.Invoke(list, new[] { processedNode });
				}
			}
			return list;
		}

		private List<T> ProcessNodeList<T>(List<Node> nodes, int index) where T:new()
		{
			return (List<T>)ProcessNodeList(typeof(List<T>), nodes, index);
		}

		private object GetValue(Type type, string val)
		{
			val = val.Trim().Replace(" ", "");
			if (type.GetTypeInfo().IsEnum)
			{
				return Helpers.GetEnum(type, val);
			}
			else if (type == typeof(int))
			{
				return int.Parse(val);
			}
			else if (type == typeof(string))
			{
				return val;
			}
			else if (type == typeof(bool))
			{
				if (bool.TryParse(val, out bool output))
				{
					return output;
				}
				else
				{
					return val == "yes";
				}
			}
			throw new Exception("Type " + type.FullName + " cannot be read as a value");
		}

		private void SetValue(PropertyInfo prop, object obj, string val, bool noSpaces = true)
		{
			val = val.Trim();
			if (noSpaces) {
				val.Replace(" ", "");
			}
			if (prop.PropertyType.GetTypeInfo().IsEnum)
			{
				prop.SetValue(obj, Helpers.GetEnum(prop.PropertyType, val));
			}
			else if (prop.PropertyType == typeof(int))
			{
				prop.SetValue(obj, int.Parse(val));
			}
			else if (prop.PropertyType == typeof(string))
			{
				prop.SetValue(obj, val.Replace("\"", ""));
			} else if(prop.PropertyType == typeof(bool))
			{
				if (bool.TryParse(val, out bool output))
				{
					prop.SetValue(obj, output);
				}
				else
				{
					prop.SetValue(obj, val == "yes");
				}

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sr">The stream reader.</param>
		/// <param name="asList">If the EuObj is a list of objects, or one object.</param>
		/// <returns></returns>
		public ParentNode ParseEuObj(StreamReader sr, bool asList = true)
		{
			var rootNode = new ParentNode(null);
			try {
				if (!asList)
				{
					return null;
				}
				
				Node currentNode = rootNode;
				ValueNode valueNode = new ValueNode(null);
				var propNameSide = true;
				while (!sr.EndOfStream)
				{
					// Test the next char
					var character = (char)sr.Read();
					if (character == ' ' || character == '\t')
					{
						continue;
					}
					if (character == '#')
					{
						while ((char)sr.Read() != '\n' && !sr.EndOfStream)
						{

						}
						propNameSide = true;
						continue;
					}
					// Will leave the stream on a space, next char should be sep
					if (propNameSide && NameChars.Contains(character))
					{
						var name = "";
						var next = character;
						while (true)
						{
							name += next;
							var peek = (char)sr.Peek();
							if (peek == '=' || peek == RightBracket || peek == '\n' || peek == '\r')
							{
								break;
							}
							next = (char)sr.Read();
						}
						var peek2 = (char)sr.Peek();
						if (peek2 == RightBracket || peek2 == '\n' || peek2 == '\r')
						{
							new ValueNode(currentNode as ParentNode) { Key = name };
						}
						valueNode.Key = name.Trim();
						continue;
					}
					// Will leave the stream on a space, next char should be newline or close bracket
					else if (!propNameSide && ValueChars.Contains(character)) {
						var value = "";
						var next = character;
						while (true)
						{
							value += next;
							var peek = (char)sr.Peek();
							if (peek == RightBracket || peek == '\r' || peek == '\n' || peek == '#')
							{
								break;
							}
							next = (char)sr.Read();
						}
						var a = new ValueNode(currentNode as ParentNode) { Key = valueNode.Key, Value = value.Trim() };
						valueNode = new ValueNode(null);
						continue;
					}
					if (character == '\n')
					{
						propNameSide = true;
						continue;
					}
					if (character == Sep)
					{
						propNameSide = false;
						continue;
					}
					if (character == LeftBracket)
					{
						var newNode = new ParentNode(currentNode as ParentNode) { Key = valueNode.Key };
						valueNode = new ValueNode(null);
						currentNode = newNode;
						propNameSide = true;
						continue;
					}
					if (character == RightBracket)
					{
						currentNode = currentNode.Parent;
						continue;
					}

				}
				return rootNode;
			} catch(Exception ex)
			{
				var brokenPoint = sr.ReadToEnd();
				throw new Exception(brokenPoint);
			}
		}
	}
}
