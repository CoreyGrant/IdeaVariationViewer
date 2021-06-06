using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.countries
{
	class CultureGroup
	{
		[PropertyName]
		public string Name { get; set; }
		[Remaining("male_names", "female_names", "graphical_culture", "second_graphical_culture", "dynasty_names", "country", "province")]
		public IReadOnlyDictionary<string, Culture> Cultures { get; set; }
	}

	class Culture
	{
		[PropertyName]
		public string Property { get; set; }
		public string Primary { get; set; }
	}
}
