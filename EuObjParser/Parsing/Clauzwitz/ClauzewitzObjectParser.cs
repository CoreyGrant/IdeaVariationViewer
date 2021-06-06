//using EuObjParser.Models.Clauzwitz.shared;
//using EuObjParser.Parsing.Clauzwitz;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;

//namespace EuObjParser.Parsing
//{
//	class ClauzewitzObjectParser
//	{
//		private readonly ObjectParserBuffer _buffer = new ObjectParserBuffer();
//		private readonly ClauzwitzObjectParserOptions _options = new ClauzwitzObjectParserOptions();
//		private readonly char _objectOpen = '{';
//		private readonly char _objectClose = '}';
//		private readonly char _nextPropChar = ' ';
//		private readonly char _objectSeperatorWrapper = ' ';
//		private readonly char _objectSeperator = '=';
//		private readonly char[] _ignoreChars = new[] { '\r', '\n', (char)65535 };
//		private readonly char _commentChar = '#';

//		/// <summary>
//		/// The Clauzwitz object format is a bit of a pain. Here shall be a documentation 
//		/// of what I've worked out so far.
//		/// 
//		/// Seperator: The seperator seems to always be '='
//		/// Object Start/End: Always seems to be '{' and '}'
//		/// NextProp: This one is nasty:
//		///    In most, this is just a newline (\n or \r and \n).
//		///    Some files don't have a newline, just a plain space ' '. This wouldn't be a problem expect some value types have spaces without quotes, so we can't just use a space as new prop.
//		///    We can, however, handle all of these funny cases, and use spaces like newlines with some considerations
//		/// PropertyNames: Basic characters, but any non-special character is fine
//		/// Values: Can include quotes, spaces inside of quotes, and some special types have
//		///    spaces without quotes. We will do these weird cases on a case-by-case basis, detecting the type in the parser alongside the property mappings
//		/// </summary>
//		/// <param name="sr"></param>
//		/// <returns></returns>
//		public T <T>Parse(string filename)
//		{

//		}

//		public List<T> ParseList<T>(string filename)
//		{
			
//		}

//		/// <summary>
//		/// Parses an object, starting with the character after the object start. Acts recursively, calling itself when it encounters
//		/// an object start and returning when it find an object close.
//		/// The type might be an object, or a list of object/primatives
//		/// </summary>
//		/// <param name="sr">A streamreader that has just passed an object start.</param>
//		/// <param name="type">The type it is to be parsed into.</param>
//		/// <param name="propertyName">The property name of the object.</param>
//		/// <param name="collapseRemaining">The remaining collapse stack.</param>
//		private object ParseObject(StreamReader sr, Type type, string propertyName, string filename, string[] collapseRemaining = null)
//		{
//			// If the type is an object
//			object typeInstance = null;
//			if (type.IsAssignableFrom(typeof(ICollection<>)))
//			{
//				return ParseCollection(sr, type, propertyName, filename);
//			}
//			else if (type == typeof(Color))
//			{
//				return ParseColor(sr);
//			}
//			else 
//			{ 
//				typeInstance = type.GetConstructor(new Type[0]).Invoke(new object[0]);
//			}
//			if (!_buffer.TypeDetailsBuffer.ContainsKey(type.FullName))
//			{
//				_buffer.TypeDetailsBuffer[type.FullName] = new TypeDetails(type, _options.PropertyNameMapping);
//			}
//			var typeDetails = _buffer.TypeDetailsBuffer[type.FullName];
//			var character = (char)sr.Read();
//			if (typeDetails.PropertyName != null)
//			{
//				typeDetails.PropertyName.SetValue(typeInstance, propertyName);
//			}
//			if (typeDetails.FileName != null)
//			{
//				typeDetails.FileName.SetValue(typeInstance, propertyName);
//			}
//			// Loop over all the properties
//			bool closeFound = false;
//			while (true)
//			{
//				var (propName, quoteInPropName) = ReadPropertyName(sr, character);
//				if (quoteInPropName)
//				{
//					continue;
//				}
//				bool isRemaining = false;
//				if (!typeDetails.PropNames.Contains(propName))
//				{
//					var remainingProps = typeDetails.RemainingProperties;
//					if (remainingProps.Key != null && !remainingProps.Key.Contains(propName))
//					{
//						isRemaining = true;
//					}
//					else {
//						continue;
//					}
//				}
//				var propValueBuffer = new StringBuilder();
//				PropertyInfo property = null;
//				string[] collapse = null;
//				if (typeDetails.Properties.ContainsKey(propName))
//				{
//					property = typeDetails.Properties[propName];
//				} else if (typeDetails.CollapseProperties.Any(x => x.Key.Contains(propName)))
//				{
//					var detail = typeDetails.CollapseProperties.Single(x => x.Key.Contains(propName));
//					collapse = detail.Key;
//					property = detail.Value;
//				}
//				var inQuotes = false;
//				while (true)
//				{
//					character = (char)sr.Read();
//					// RECURSIVE OBJECT
//					if (character == _objectOpen)
//					{
//						// What prop type is it?
//						if(collapse != null)
//						{
//							// Collapse wants to loop over the collapse list, and 
//							// parse the deepest result as the type
//							// If the collapse list is of size one, or the collapse list 
//							// passed in is of size one, then pass in the property type,
//							// otherwise pass in the main type with reduced collapse list
//							Type propType;
//							if(collapseRemaining != null && collapseRemaining.Any())
//							{
//								// Deep in the loop, check to see if there is 1 left
//								if (collapseRemaining.Length == 1)
//								{
//									propType = property.PropertyType;
//								}
//								else
//								{
//									propType = type;
//								}
//							} else
//							{
//								if(collapse.Length == 1)
//								{
//									propType = property.PropertyType;
//								}
//								else
//								{
//									propType = type;
//								}
//							}
//							var value = ParseObject(
//								sr,
//								propType,
//								collapse.First(),
//								filename,
//								collapse.Skip(1).ToArray());
//							property.SetValue(typeInstance, value);
//						}
//						if (isRemaining)
//						{
//							// Its an object which is a remaining type
//							var remainingProps = typeDetails.RemainingProperties;
//							object remainingList = remainingProps.Value.GetValue(typeInstance);
//							var listGenericType = remainingProps.Value.PropertyType.GetGenericArguments()[0];

//							var addToList = remainingProps.Value.PropertyType.GetMethods().Single(x => x.Name == "Add");
//							if (remainingList == null)
//							{
//								var list = remainingProps.Value.PropertyType.GetConstructor(new Type[0]).Invoke(new object[0]);
								
//								var value = ParseObject(
//									sr,
//									listGenericType,
//									collapse.First(),
//									filename,
//									collapse.Skip(1).ToArray());
//									property.SetValue(typeInstance, value);
//								addToList.Invoke(list, new object[] { value });
//								typeDetails.RemainingProperties.Value.SetValue(typeInstance, list);
//							}
//							else
//							{
//								var value = ParseObject(
//									sr,
//									listGenericType,
//									collapse.First(),
//									filename,
//									collapse.Skip(1).ToArray());
//								property.SetValue(typeInstance, value);
//								addToList.Invoke(remainingList, new object[] { value });
//								typeDetails.RemainingProperties.Value.SetValue(typeInstance, remainingList);
//							}
//						}
//						// its just a normal property
//						property.SetValue(typeInstance, ParseObject(sr, property.PropertyType, propName, filename));
//					}
//					else if (_ignoreChars.Contains(character))
//					{
//						break;
//					} else if(character == ' ') 
//					{
//						if (!inQuotes) { break; }
//					}
//					else if (character == '"')
//					{
//						inQuotes = !inQuotes;
//						continue;
//					} 
//					else  if (character == _commentChar && !inQuotes)
//					{
//						// read until a new line
//						while (true)
//						{
//							if (_ignoreChars.Contains(character))
//							{
//								break;
//							}
//							character = (char)sr.Read();
//						}
//						break;
//					} else if (character == _objectClose && !inQuotes)
//					{
//						closeFound = true;
//						break;
//					}
//					propValueBuffer.Append(character);
//				}
//				// STRING VALUE
//				if (isRemaining)
//				{
//					// Its a string value remaining property
//					var remainingProps = typeDetails.RemainingProperties;
//					object remainingList = remainingProps.Value.GetValue(typeInstance);
//					var listGenericType = remainingProps.Value.PropertyType.GetGenericArguments()[0];

//					var addToList = remainingProps.Value.PropertyType.GetMethods().Single(x => x.Name == "Add");
//					if (remainingList == null)
//					{
//						var list = remainingProps.Value.PropertyType.GetConstructor(new Type[0]).Invoke(new object[0]);
//						addToList.Invoke(list, new object[] { ParseStringValue(propValueBuffer.ToString(), listGenericType) });
//						typeDetails.RemainingProperties.Value.SetValue(typeInstance, list);
//					}
//					else
//					{
//						addToList.Invoke(remainingList, new object[] { ParseStringValue(propValueBuffer.ToString(), listGenericType) });
//						typeDetails.RemainingProperties.Value.SetValue(typeInstance, remainingList);
//					}
//					continue;
//				}
//				property.SetValue(typeInstance, ParseStringValue(propValueBuffer.ToString(), property.PropertyType));
//				if (closeFound)
//				{
//					return typeInstance;
//				}
//			}
//		}

//		private object ParseCollection(StreamReader sr, Type type, string propertyName, string filename)
//		{
//			var genericType = type.GetGenericArguments()[0];
//			var collectionInstance = type.GetConstructor(new Type[0]).Invoke(new object[0]);
//			var add = type.GetMethods().Single(x => x.Name == "Add");
//			var character = (char)sr.Read();
//			var objectCloseFound = false;
//			while (true)
//			{
//				var (propName, quoteInPropName) = ReadPropertyName(sr, character);
//				if (quoteInPropName)
//				{
//					continue;
//				}
//				var propValueBuffer = new StringBuilder();
//				bool inQuotes = false;
//				while (true)
//				{
//					if((_ignoreChars.Contains(character) || character == ' ') && !inQuotes)
//					{
//						break;
//					} else if(character == _objectClose && !inQuotes)
//					{
//						objectCloseFound = true;
//						break;
//					} else if(character == _objectOpen)
//					{
//						var value = ParseObject(sr, genericType, propName, filename);
//						add.Invoke(collectionInstance, new object[] { value });
//					} else if(character == '"')
//					{
//						inQuotes = !inQuotes;
//					} else if (character == _commentChar && !inQuotes)
//					{
//						// read until a new line
//						while (true)
//						{
//							if (_ignoreChars.Contains(character))
//							{
//								break;
//							}
//							character = (char)sr.Read();
//						}
//						break;
//					}
//					else
//					{
//						propValueBuffer.Append(character);
//					}
//					character = (char)sr.Read();
//				}
//				if(genericType.IsValueType || genericType == typeof(string))
//				{
//					add.Invoke(collectionInstance, new object[] { ParseStringValue(propValueBuffer.ToString(), genericType) });
//				}
//				else
//				{
//					add.Invoke(collectionInstance, new object[] { });
//				}
				
//				if (objectCloseFound)
//				{
//					break;
//				}
//			}
//			return collectionInstance;
//		}

//		private Color ParseColor(StreamReader sr)
//		{
//			var character = (char)sr.Read();
//			// Read off spaces/newlines
//			while (true)
//			{
//				if(!_ignoreChars.Contains(character) && character != ' ')
//				{
//					break;
//				}
//				character = (char)sr.Read();
//			}
//			// Red
//			string redStr = "";
//			while (true)
//			{
//				if(_ignoreChars.Contains(character) || character == ' ')
//				{
//					break;
//				}
//				redStr += character;
//				character = (char)sr.Read();
//			}
//			// Read off spaces/newlines
//			while (true)
//			{
//				if (!_ignoreChars.Contains(character) && character != ' ')
//				{
//					break;
//				}
//				character = (char)sr.Read();
//			}
//			// Green
//			string greenStr = "";
//			while (true)
//			{
//				if (_ignoreChars.Contains(character) || character == ' ')
//				{
//					break;
//				}
//				greenStr += character;
//				character = (char)sr.Read();
//			}
//			// Read off spaces/newlines
//			while (true)
//			{
//				if (!_ignoreChars.Contains(character) && character != ' ')
//				{
//					break;
//				}
//				character = (char)sr.Read();
//			}
//			// Blue
//			string blueStr = "";
//			while (true)
//			{
//				if (_ignoreChars.Contains(character) || character == ' ' || character == _objectClose)
//				{
//					break;
//				}
//				blueStr += character;
//				character = (char)sr.Read();
//			}
//			return new Color { Red = int.Parse(redStr), Green = int.Parse(greenStr), Blue = int.Parse(blueStr) };
//		}

//		private (string, bool) ReadPropertyName(StreamReader sr, char character)
//		{
//			var propNameBuffer = new StringBuilder();
//			// First we need to get the name for this property
//			bool quoteInPropName = false;
//			while (true)
//			{
//				// We are reading the prop name, we can read until we get to a space
//				propNameBuffer.Append(character);
//				character = (char)sr.Read();
//				if (character == _commentChar)
//				{
//					// read until a new line
//					while (true)
//					{
//						if (_ignoreChars.Contains(character))
//						{
//							break;
//						}
//						character = (char)sr.Read();
//					}
//					// If we find a quote inside a property name, its an error and we should just ignore the line
//					quoteInPropName = true;
//					break;
//				}
//				if (character == _objectSeperatorWrapper || character == _objectSeperatorWrapper)
//				{
//					// We've reached the end of the property, now we go to the end of the sep and it's wrapper and start reading the value
//					while (true)
//					{
//						var peeked = (char)sr.Peek();
//						if (peeked != _objectSeperatorWrapper && peeked != _objectSeperator)
//						{
//							break;
//						}
//						character = (char)sr.Read();
//					}
//					break;
//				}
//			}
//			return (propNameBuffer.ToString().Trim(), quoteInPropName);
//		}

//		private object ParseStringValue(string value, Type type)
//		{
//			if(type == typeof(string))
//			{
//				return value;
//			} else if(type == typeof(int))
//			{
//				return int.Parse(value);
//			}
//			else if (type == typeof(long))
//			{
//				return long.Parse(value);
//			}
//			else if (type == typeof(decimal))
//			{
//				return decimal.Parse(value);
//			}
//			else if (type == typeof(bool))
//			{
//				return value.ToLower() == "yes";
//			}
//			throw new Exception("Didn't recognise type of string value");
//		}
//	}

//	class ObjectParserBuffer
//	{
//		public Dictionary<string, TypeDetails> TypeDetailsBuffer { get; set; } = new Dictionary<string, TypeDetails>();
//		public bool InQuote { get; private set; }

//		public void ToggleQuote() { InQuote = !InQuote; }

//		public void Reset ()
//		{
//			InQuote = false;
//			TypeDetailsBuffer = new Dictionary<string, TypeDetails>();
//			PropName = new StringBuilder();
//			PropValue = new StringBuilder();
//		}

//		public StringBuilder PropName { get; private set; } = new StringBuilder();
//		public StringBuilder PropValue { get; private set; } = new StringBuilder();
//	}

//	class TypeDetails
//	{
//		private readonly Type _type;
//		private readonly Func<string, string> _propertyNameMapping;

//		public TypeDetails(Type type, Func<string, string> propertyNameMapping)
//		{
//			_type = type;
//			_propertyNameMapping = propertyNameMapping;
//			ParseProperties();
//		}

//		public HashSet<string> PropNames { get; private set; } = new HashSet<string>();

//		public PropertyInfo PropertyName { get; private set; }
//		public PropertyInfo FileName { get; private set; }
//		public Dictionary<string, PropertyInfo> Properties { get; private set; }
//		public Dictionary<string[], PropertyInfo> CollapseProperties { get; private set; }
//		public KeyValuePair<string[], PropertyInfo> RemainingProperties { get; private set; }

//		private void ParseProperties()
//		{
//			var properties = _type.GetProperties();
//			Properties = new Dictionary<string, PropertyInfo>();
//			CollapseProperties = new Dictionary<string[], PropertyInfo>();
//			foreach (var property in properties)
//			{
//				if (property.GetCustomAttribute<PropertyNameAttribute>() != null)
//				{
//					PropertyName = property;
//					PropNames.Add(property.Name);
//				} else if (property.GetCustomAttribute<FileNameAttribute>() != null)
//				{
//					FileName = property;

//					PropNames.Add(property.Name);
//				} else if (property.GetCustomAttribute<RemainingAttribute>() != null)
//				{
//					var att = property.GetCustomAttribute<RemainingAttribute>();
//					RemainingProperties = KeyValuePair.Create(att.Except, property);

//					PropNames.Add(property.Name);
//				}
//				else if (property.GetCustomAttribute<ValueAttribute>() != null)
//				{
//					var att = property.GetCustomAttribute<ValueAttribute>();
//					PropNames.Add(property.Name);
//					if (att.Collapse != null && att.Collapse.Any())
//					{
//						CollapseProperties[att.Collapse] = property;
//					}
//					else
//					{
//						Properties[att.Name] = property;
//					}
//				}
//				else
//				{
//					PropNames.Add(property.Name);
//					Properties[_propertyNameMapping(property.Name)] = property;
//				}
//			}
			

//			if (Debugger.IsAttached)
//			{
//				Validate();
//			}
//		}

//		private void Validate()
//		{

//		}
//	}
//}
