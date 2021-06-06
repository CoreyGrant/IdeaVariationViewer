using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Models.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Output.Countries
{
	class Country
	{
		public string Tag { get; set; }
		public string Name { get; set; }
		public IReadOnlyCollection<Idea> Ideas { get; set; }
		public IReadOnlyCollection<Color> Colors { get; set; }
	}
}
