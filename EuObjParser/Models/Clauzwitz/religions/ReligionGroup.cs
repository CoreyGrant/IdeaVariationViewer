using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.religions
{
	class ReligionGroup
	{
		[PropertyName]
		public string Name { get; set; }
		public bool DefenderOfFaith { get; set; }
		public bool CanFormPersonalUnions { get; set; }
		public int FlagsWithEmblemPercentage { get; set; }
		public int CenterOfReligion { get; set; }
		public string CrusadeName { get; set; }
		public string HarmonizedModifier { get; set; }
		[Remaining()]
		public List<Religion> Religions { get; set; }
	}

	class Religion
	{
		[PropertyName]
		public string Name { get; set; }
		public Color Color { get; set; }
		public int Icon { get; set; }
		public IReadOnlyDictionary<string, string> Country { get; set; }
		public IReadOnlyDictionary<string, string> SecondaryCountry { get; set; }
		public IReadOnlyDictionary<string, string> Province { get; set; }
		public IReadOnlyCollection<string> Heretic { get; set; }
		public bool PersonalDiety { get; set; }
		public bool UsesAnglicanPower { get; set; }
		public string AspectsName { get; set; }
		public IReadOnlyCollection<string> Aspects { get; set; }
		public IReadOnlyCollection<string> AllowedConversion { get; set; }
		public IReadOnlyCollection<string> AllowedCenterConversion { get; set; }
		public IReadOnlyCollection<string> Blessings { get; set; }
		public IReadOnlyCollection<int> HolySites { get; set; }
	}
}
