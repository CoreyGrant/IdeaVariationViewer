using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.countries
{
	class Country
	{
		[PropertyName]
		public string Name { get; set; }
		public Color Color { get; set; }
	}
}
