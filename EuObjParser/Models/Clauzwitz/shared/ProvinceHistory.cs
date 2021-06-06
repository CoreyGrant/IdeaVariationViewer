using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EuObjParser.Models.Clauzwitz.shared
{
	class ProvinceHistory
	{
		private static Regex NumberRegex = new Regex("^[0-9]+");
		[PropertyName]
		public string FileName { get; set; }
		public int Number => int.Parse(NumberRegex.Match(FileName).Value);
		public string Owner { get; set; }
		public decimal BaseTax { get; set; }
		public decimal BaseManpower { get; set; }
		public decimal BaseProduction { get; set; }
		public string Capital { get; set; }
	}
}
