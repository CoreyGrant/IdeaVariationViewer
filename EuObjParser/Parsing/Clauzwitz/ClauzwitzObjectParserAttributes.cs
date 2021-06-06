using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EuObjParser.Parsing.Clauzwitz
{
	/// <summary>
	/// Takes the value of the property name for the object being parsed.
	/// If is part of an object, will take the prop name for that object
	/// If is part of a collection, will take the propname for that kv pair
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	class PropertyNameAttribute : Attribute
	{
		public PropertyNameAttribute()
		{

		}
	}

	/// <summary>
	/// Takes the value of the whole object.
	/// Used in properties that are part of collections, to make the mapping more compact.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	class PropertyValueAttribute : Attribute
	{
		public string Name { get; }
		public PropertyValueAttribute(string name)
		{
			Name = name;
		}
	}

	/// <summary>
	/// Takes any data properties not handled by other properties and reads them as
	/// a list, excluding the except array.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	class RemainingAttribute : Attribute
	{
		public RemainingAttribute(params string[] except)
		{
			Except = except;
		}

		public string[] Except { get; }
	}

	class PropAttributeDetails
	{
		private readonly Type _type;

		public PropAttributeDetails(Type type, bool validate = false)
		{
			_type = type;
			if (validate)
			{
				Validate();
			}
			var properties = type.GetProperties();
			var propValues = GetProps<PropertyValueAttribute>(properties);
			var remaining = GetProps<RemainingAttribute>(properties).SingleOrDefault();
			var propName = GetProps<PropertyNameAttribute>(properties).SingleOrDefault();
			var defaultProps = properties.Where(x => !x.GetCustomAttributes().Any());
			foreach(var defaultProp in defaultProps)
			{
				Names[MapPropName(defaultProp.Name)] = defaultProp;
			}
			if (propValues.Any())
			{
				foreach(var prop in propValues)
				{
					var valueAtt = GetCustomAttributes<PropertyValueAttribute>(prop).Single();
					Names[valueAtt.Name] = prop;
				}
			}
			if (propName != null)
			{
				PropertyName = propName;
			}

			if(remaining != null)
			{
				Remaining = (GetCustomAttributes<RemainingAttribute>(remaining).Single().Except.Concat(Names.Keys).ToArray(), remaining);
			}
		}

		public PropertyInfo PropertyName { get; }
		public Dictionary<string, PropertyInfo> Names { get; } = new Dictionary<string, PropertyInfo>();
		public (string[], PropertyInfo) Remaining { get; }
		private void Validate()
		{
			var properties = _type.GetProperties();
			if(GetProps<RemainingAttribute>(properties).Count() > 1)
			{
				throw new Exception("Cannot have more than one RemainingAttibute per class");
			}
			if(GetProps<PropertyNameAttribute>(properties).Count() > 1)
			{
				throw new Exception("Cannot have more than one PropertyName per class");
			}
		}

		private IEnumerable<PropertyInfo> GetProps<T>(IEnumerable<PropertyInfo> properties)
		{
			return properties.Where(x => x.GetCustomAttributes(typeof(T), false).Any());
		}

		private IEnumerable<T> GetCustomAttributes<T>(PropertyInfo pi)
		{
			return pi.GetCustomAttributes(typeof(T), false).Cast<T>();
		}

		private string MapPropName(string propName)
		{
			while (true)
			{
				var indexOfUpperCase = propName
					.Select((x, i) => new { u = char.IsUpper(x), i, c = x })
					.FirstOrDefault(x => x.u) ?? new { u = false, i = -1, c = (char)0 };
				if (indexOfUpperCase.i == -1)
				{
					break;
				}
				propName = propName.Remove(indexOfUpperCase.i, 1).Insert(indexOfUpperCase.i, (indexOfUpperCase.i == 0 ? "" : "_") + indexOfUpperCase.c.ToString().ToLower());

			}
			return propName.ToLower();
		}
	}
}
