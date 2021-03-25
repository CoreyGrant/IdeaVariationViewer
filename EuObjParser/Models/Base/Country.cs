using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EuObjParser.Attributes;
using Newtonsoft.Json;

namespace EuObjParser.Models.Base
{
	class Country
	{
		public string Name => Regex.Replace(NameWithExt, ".\\w+$", "");

		[EuObjPropName]
		[JsonIgnore]
		public string NameWithExt { get; set; }
		[EuPropName("color")]
		public Color Color { get; set; }
	}

	class Color
	{
		[JsonIgnore]
		[EuObjPropValue]
		public string Value { get; set; }

		public string RGB => $"rgb({Red},{Green},{Blue})";
		public int Red => int.Parse(Regex.Replace(Value, " {2,}", " ").Split(" ")[0]);
		public int Green => int.Parse(Regex.Replace(Value, " {2,}", " ").Split(" ")[1]);
		public int Blue => int.Parse(Regex.Replace(Value, " {2,}", " ").Split(" ")[2]);
	}
}
