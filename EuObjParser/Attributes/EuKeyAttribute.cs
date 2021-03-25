using EuObjParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Attributes
{
	class EuKeyAttribute : Attribute
	{
		public string Name { get; }
		public EuKeyAttribute(string name) { Name = name; }
	}

	class EuKeyReplaceAttribute : Attribute
	{
		public string Value { get; }
		public EuKeyReplaceAttribute(string value) { Value = value; }
	}

	class EuValueTypeAttribute: Attribute
	{
		public BonusDisplayType Type { get; }
		public EuValueTypeAttribute(BonusDisplayType type) { Type = type; }
	}

	class EuImageAttribute: Attribute
	{
		public string Url { get; }
		public EuImageAttribute(string url) { Url = url; }
	}
}
