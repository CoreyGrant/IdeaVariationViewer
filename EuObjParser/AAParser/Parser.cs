using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Parsing;
using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static EuObjParser.AAParser.ParserUtils;

namespace EuObjParser.AAParser
{
	class Parser
	{
		private readonly ClauzwitzObjectParserOptions options = new ClauzwitzObjectParserOptions();
		private Dictionary<Type, PropAttributeDetails> propAttributeDetails = new Dictionary<Type, PropAttributeDetails>();
		public Parser()
		{
		}

		public T Parse<T>(string filename, string fullFolderPath)
		{
			using (var sr = new StreamReader(File.OpenRead(Path.Combine(fullFolderPath, filename)), ParserUtils.Encoding.Windows1252))
			{
				return (T)Parse(sr, typeof(T), filename);
			}
		}

		private object Parse(StreamReader sr, Type type, string propName)
		{
			// Horrible, make ReadOnlyCollection detection proper
			if (type.IsGenericType &&
				type.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>))
			{
				return ParseObjectAsCollection(sr, type);
			} else if(type == typeof(Color))
			{
				return ParseColor(sr);
			} else if(type == typeof(Trigger))
			{
				return ParseTrigger(sr);
			}
			else
			{
				return ParseObject(sr, type, propName);
			}
		}

		/// <summary>
		/// Parses an object into a ReadOnlyCollection
		///  - An IReadOnlyCollection of value types will expect no prop seperators, just values
		///  - An IReadOnlyCollection of key/value pairs or IReadOnlyDictionary will
		///    map all key/values as expected
		///  - An IReadOnlyCollection of objects will read the seperate objects
		/// </summary>
		/// <param name="sr">The stream reader, having just read the object start.</param>
		/// <param name="type">The type of the collection.</param>
		/// <returns></returns>
		private object ParseObjectAsCollection(StreamReader sr, Type type)
		{
			var genericTypeArguments = type.GetGenericArguments();
			if(genericTypeArguments.Length == 2 && type.IsAssignableFrom(typeof(IReadOnlyDictionary<,>).MakeGenericType(genericTypeArguments[0], genericTypeArguments[1])))
			{
				// Dictionary
				var t1 = type.GetGenericArguments()[0];
				var t2 = type.GetGenericArguments()[1];

				var dictType = typeof(Dictionary<,>).MakeGenericType(t1, t2);
				var instance = dictType.GetConstructor(new Type[0]).Invoke(new object[0]);
				var kvPairType = typeof(KeyValuePair<,>).MakeGenericType(t1, t2);
				var add = dictType.GetMethod("Add");
				while (true)
				{
					var kvPair = kvPairType
						.GetConstructor(new Type[0]).Invoke(new object[0]);
					var character = (char)sr.Read();
					character = SkipUntilNot(sr, character, NewLines.Concat(Spaces));
					if (character == ObjectClose)
					{
						break;
					}
					string propName;
					(character, propName) = ReadPropertyName(sr, character);
					if(t2 == typeof(string) || t2.IsValueType)
					{
						string propValue;
						(character, propValue) = ReadPropertyStringValue(sr, character);
						kvPairType.GetProperty("Key").SetValue(kvPair, propName);
						kvPairType.GetProperty("Value").SetValue(kvPair, propValue);
						add.Invoke(instance, new[] { kvPair });
					}
					else
					{
						character = SkipUntil(sr, character, new[] { ObjectOpen });
						var propValue = Parse(sr, t2, propName);
						kvPairType.GetProperty("Key").SetValue(kvPair, propName);
						kvPairType.GetProperty("Value").SetValue(kvPair, propValue);
						add.Invoke(instance, new[] { kvPair });
					}
				}
				return instance;
			} else 
			{
				var genericType = type.GetGenericArguments()[0];
				var genericTypeGenericArgs = genericType.GetGenericArguments();
				if (genericTypeGenericArgs.Count() == 2 && genericType.IsAssignableFrom(typeof(KeyValuePair<,>).MakeGenericType(
					genericTypeGenericArgs[0], genericTypeGenericArgs[1])))
				{
					var t1 = genericType.GetGenericArguments()[0];
					var t2 = genericType.GetGenericArguments()[1];
					// collection of k/v pairs
					var listType = typeof(List<>).MakeGenericType(genericType);
					var instance = listType.GetConstructor(new Type[0]).Invoke(new object[0]);
					var kvPairType = typeof(KeyValuePair<,>).MakeGenericType(t1, t2);
					var add = listType.GetMethod("Add");
					while (true)
					{
						
						var character = (char)sr.Read();
						character = SkipUntilNot(sr, character, NewLines.Concat(Spaces));
						if (character == ObjectClose || character == Eof)
						{
							break;
						}
						string propName;
						(character, propName) = ReadPropertyName(sr, character);
						if (t2 == typeof(string) || t2.IsValueType)
						{
							string propValue;
							(character, propValue) = ReadPropertyStringValue(sr, character);
							var kvPair = kvPairType
							.GetConstructor(new Type[] { t1, t2 }).Invoke(new object[] { propName, propValue });
							add.Invoke(instance, new[] { kvPair });
						}
						else
						{
							character = SkipUntil(sr, character, new[] { ObjectOpen });
							var propValue = Parse(sr, t2, propName);
							var kvPair = kvPairType
							.GetConstructor(new Type[] { t1, t2 }).Invoke(new object[] { propName, propValue });
							add.Invoke(instance, new[] { kvPair });
						}
					}
					return instance;
				}
				else if (genericType == typeof(string) || genericType.IsValueType)
				{
					// List of value types
					var listType = typeof(List<>).MakeGenericType(genericType);
					var list = listType.GetConstructor(new Type[0]).Invoke(new object[0]);
					var add = listType.GetMethod("Add");
					while (true)
					{
						var character = (char)sr.Read();
						character = SkipUntilNot(sr, character, NewLines.Concat(Spaces));
						if (character == ObjectClose || character == Eof)
						{
							break;
						}
						string propValue;
						(character, propValue) = ReadPropertyStringValue(sr, character);
						add.Invoke(list, new[] { ParseStringValue(propValue, genericType) });
					}
					return list;
				}
				else
				{
					// List of objects
					var listType = typeof(List<>).MakeGenericType(genericType);
					var list = listType.GetConstructor(new Type[0]).Invoke(new object[0]);
					var add = listType.GetMethod("Add");
					while (true)
					{
						var character = (char)sr.Read();
						character = SkipUntilNot(sr, character, NewLines.Concat(Spaces));
						if (character == ObjectClose || character == Eof)
						{
							break;
						}
						string propName;
						(character, propName) = ReadPropertyName(sr, character);
						var value = Parse(sr, genericType, propName);
						add.Invoke(list, new[] { value });
					}
					return list;
				}
			}
		}
		private object ParseObject(StreamReader sr, Type type, string objPropName)
		{
			var instance = type.GetConstructor(new Type[0]).Invoke(new object[0]);
			if (!propAttributeDetails.ContainsKey(type))
			{
				propAttributeDetails[type] = new PropAttributeDetails(type);
			}
			var propDetails = propAttributeDetails[type];
			if (propDetails.PropertyName != null)
			{
				propDetails.PropertyName.SetValue(instance, objPropName);
			}
			object remainingList = null;
			MethodInfo add = null;
			Type remainingGenericType = null;
			Type secondGenericType = null;
			if (propDetails.Remaining.Item2 != null) 
			{
				var propType = propDetails.Remaining.Item2.PropertyType;
				var propGenericTypeArguments = propType.GetGenericArguments();
				remainingGenericType = propGenericTypeArguments[0];
				if (propGenericTypeArguments.Length == 2 && propType.IsAssignableFrom(typeof(IReadOnlyDictionary <,>).MakeGenericType(remainingGenericType, propGenericTypeArguments[1])))
				{
					secondGenericType = propGenericTypeArguments[1];
					var kvpType = typeof(KeyValuePair<,>).MakeGenericType(remainingGenericType, secondGenericType);
					var listType = typeof(List<>).MakeGenericType(kvpType);
					remainingList = listType.GetConstructor(new Type[0]).Invoke(new object[0]);
					add = listType.GetMethod("Add");
				}
				else {
					var listType = typeof(List<>).MakeGenericType(remainingGenericType);
					remainingList = listType.GetConstructor(new Type[0]).Invoke(new object[0]);
					add = listType.GetMethod("Add");
				}
				
				
			}
			while (true)
			{
				var character = (char)sr.Read();
				character = SkipUntilNot(sr, character, NewLines.Concat(Spaces));
				if (character == ObjectClose || character == Eof)
				{
					break;
				}
				string propName;
				(character, propName) = ReadPropertyName(sr, character);
				PropertyInfo prop = null;
				bool remaining = false;
				if (propDetails.Names.ContainsKey(propName))
				{
					prop = propDetails.Names[propName];
				} else if(propDetails.Remaining.Item1 != null && !propDetails.Remaining.Item1.Contains(propName))
				{
					remaining = true;
				}
				if (character == ObjectOpen)
				{
					if(prop != null)
					{
						var value = Parse(sr, prop.PropertyType, propName);
						prop.SetValue(instance, value);
					} else if (remaining)
					{
						if (secondGenericType != null)
						{
							var value = Parse(sr, secondGenericType, propName);
							var kvpType = typeof(KeyValuePair<,>).MakeGenericType(remainingGenericType, secondGenericType);
							var kvp = kvpType.GetConstructor(new Type[] { remainingGenericType, secondGenericType})
								.Invoke(new object[] { propName, value });
							add.Invoke(remainingList, new[] { kvp });
						}
						else
						{
							var value = Parse(sr, remainingGenericType, propName);
							add.Invoke(remainingList, new[] { value });
						}
					}
					else
					{
						(character) = SkipUntil(sr, character, new[] { ObjectClose, Eof });
					}
				}
				else
				{
					if (prop != null)
					{
						string propValue;
						(character, propValue) = ReadPropertyStringValue(sr, character);
						if (prop.PropertyType.IsGenericType &&
							prop.PropertyType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>))
						{
							var gt = prop.PropertyType.GetGenericArguments()[0];
							var listType = typeof(List<>).MakeGenericType(gt);
							var addToList = listType.GetMethod("Add");
							object list = prop.GetValue(instance);
							if(list == null)
							{
								list = listType.GetConstructor(new Type[0]).Invoke(new object[0]);
							}
							addToList.Invoke(list, new[] { ParseStringValue(propValue, gt) });
							prop.SetValue(instance, list);
						}
						else
						{
							prop.SetValue(instance, ParseStringValue(propValue, prop.PropertyType));
						}
					}
					else if (remaining)
					{
						string propValue;
						(character, propValue) = ReadPropertyStringValue(sr, character);
						if (secondGenericType != null)
						{
							var kvpType = typeof(KeyValuePair<,>).MakeGenericType(remainingGenericType, secondGenericType);
							var kvp = kvpType.GetConstructor(new Type[] { remainingGenericType, secondGenericType})
								.Invoke(new object[] { propName, propValue});
							add.Invoke(remainingList, new[] { kvp });
						}
						else
						{
							add.Invoke(remainingList, new[] { ParseStringValue(propValue, remainingGenericType) });
						}
					}
					else
					{
						(character, _) = ReadPropertyStringValue(sr, character);
					}
				}
			}
			if (remainingList != null)
			{
				if(secondGenericType != null)
				{
					// We have a dictionary
					var dictType = typeof(IDictionary<,>).MakeGenericType(remainingGenericType, secondGenericType);
					var kvEnumType = typeof(IEnumerable<>).MakeGenericType(typeof(KeyValuePair<,>).MakeGenericType(remainingGenericType, secondGenericType));
					var dict = typeof(Dictionary<,>).MakeGenericType(remainingGenericType, secondGenericType).GetConstructor(new Type[] { kvEnumType })
						.Invoke(new[] { remainingList });
					remainingList = typeof(ReadOnlyDictionary<,>).MakeGenericType(remainingGenericType, secondGenericType).GetConstructor(new Type[] { dictType }).Invoke(new[] { dict });
				}
				propDetails.Remaining.Item2.SetValue(instance, remainingList);
			}
			return instance;
		}

		#region Color

		private object ParseColor(StreamReader sr) 
		{
			string red, green, blue;
			var character = (char)sr.Read();
			// Skip any leading spaces
			var spaceChars = NewLines.Concat(Spaces);
			character = SkipUntilNot(sr, character, spaceChars);
			(character, red) = ReadUntil(sr, character, spaceChars);

			character = SkipUntilNot(sr, character, spaceChars);
			(character, green) = ReadUntil(sr, character, spaceChars);

			character = SkipUntilNot(sr, character, spaceChars);
			(character, blue) = ReadUntil(sr, character, spaceChars.Concat(new[] { ObjectClose }));
			SkipUntil(sr, character, new[] { ObjectClose });
			return new Color { Red = int.Parse(red), Green = int.Parse(green), Blue = int.Parse(blue) };
		}

		#endregion

		#region Trigger
		private object ParseTrigger(StreamReader sr) 
		{
			var conditions = new List<TriggerCondition>();
			var conditionSets = new List<TriggerConditionSet>();
			// Loop over properties
			while (true)
			{
				var character = (char)sr.Read();
				character = SkipUntilNot(sr, character, NewLines.Concat(Spaces));
				if(character == ObjectClose)
				{
					break;
				}
				string propName;
				(character, propName) = ReadPropertyName(sr, character);
				if(character == ObjectOpen)
				{
					var composeOr = false;
					var modifierNot = false;
					if(propName.Equals("not", StringComparison.OrdinalIgnoreCase))
					{
						modifierNot = true;
					}
					else if (propName.Equals("or", StringComparison.OrdinalIgnoreCase))
					{
						composeOr = true;
					}
					var cs = ParseTriggerConditionSet(sr, false);
					conditionSets.Add(cs);
					cs.ComposeOr = composeOr;
					cs.ModifierNot = modifierNot;
				}
				else
				{
					string propValue;
					(character, propValue) = ReadPropertyStringValue(sr, character);
					conditions.Add(new TriggerCondition { Name = propName, Value = propValue });
				}
			}
			return new Trigger { Conditions = conditions, ConditionSets = conditionSets };
		}

		private TriggerConditionSet ParseTriggerConditionSet(StreamReader sr, bool prevCompose = false)
		{
			var conditions = new List<TriggerCondition>();
			var conditionSets = new List<TriggerConditionSet>();
			var composeOr = prevCompose;
			var modifierNot = false;
			// Loop over properties
			while (true)
			{
				var character = (char)sr.Read();
				character = SkipUntilNot(sr, character, NewLines.Concat(Spaces));
				if (character == ObjectClose)
				{
					break;
				}
				string propName;
				(character, propName) = ReadPropertyName(sr, character);
				if (character == ObjectOpen)
				{
					if (propName.Equals("not", StringComparison.OrdinalIgnoreCase))
					{
						modifierNot = true;
					}
					else if (propName.Equals("or", StringComparison.OrdinalIgnoreCase))
					{
						composeOr = true;
					}
					else if (propName.Equals("and", StringComparison.OrdinalIgnoreCase))
					{
						composeOr = false;
					}
					var cs = ParseTriggerConditionSet(sr, composeOr);
					cs.ComposeOr = composeOr;
					cs.ModifierNot = modifierNot;
					conditionSets.Add(cs);
				}
				else
				{
					string propValue;
					(character, propValue) = ReadPropertyStringValue(sr, character);
					conditions.Add(new TriggerCondition { Name = propName, Value = propValue });
				}
			}
			return new TriggerConditionSet { Conditions = conditions, ConditionSets = conditionSets };
		}

		#endregion


	}
}
