using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Attributes
{
	/// <summary>
	/// Takes the value from the property name of the current object
	/// </summary>
	class EuObjPropNameAttribute : Attribute
	{
	}

	/// <summary>
	/// Takes the value from the property in the current object with the name
	/// </summary>
	class EuPropNameAttribute : Attribute
	{
		public EuPropNameAttribute(string name) { Name = name; }
		public string Name { get; }
	}

	/// <summary>
	/// Takes the properties not covered by other attributes, and without the names supplied
	/// </summary>
	class EuPropNameRemainingAttribute : Attribute
	{
		public string[] Except { get; }
		public EuPropNameRemainingAttribute(string except = null)
		{
			Except = string.IsNullOrEmpty(except)
				? new string[0]
				: except.Split(',');
		}
	}

	/// <summary>
	/// Takes the value of the index of the current property
	/// </summary>
	class EuIndexAttribute : Attribute
	{
		public EuIndexAttribute()
		{
		}
	}

	/// <summary>
	/// Takes the value of the current property
	/// If a value property should be on a value type
	/// If an object property should be a sub-object
	/// </summary>
	class EuObjPropValueAttribute: Attribute
	{

	}

	/// <summary>
	/// Takes the values from the properties with given name
	/// </summary>
	class EuObjListAttribute : Attribute
	{
		public EuObjListAttribute(string name) { Name = name; }
		public string Name { get; }
	}

	/// <summary>
	/// Collapses the specified path down to the root object
	/// Only works in conjunction with EuObjList
	/// </summary>
	class EuObjCollapseAttribute : Attribute
	{
		public string[] PropNames { get; }
		public EuObjCollapseAttribute(string propNames = null)
		{
			PropNames = string.IsNullOrEmpty(propNames)
				? new string[0]
				: propNames.Split(',');
		}
	}
}
