using EuObjParser.Parsing.Clauzwitz;
using System.Collections.Generic;

namespace EuObjParser.Models.Clauzwitz.countries
{
	class CountryHistory
	{
		[PropertyName]
		public string FileName { get; set; }
		public string Government { get; set; }
		public int GovernmentRank { get; set; }
		public int Mercantilism { get; set; }
		public string TechnologyGroup { get; set; }
		public IReadOnlyCollection<string> AddGovernmentReform { get; set; }
		public string Religion { get; set; }
		public string PrimaryCulture { get; set; }
		public int Capital { get; set; }
		public int FixedCapital { get; set; }
		public string UnitType { get; set; }
		public string ReligiousSchool { get; set; }
	}
}
