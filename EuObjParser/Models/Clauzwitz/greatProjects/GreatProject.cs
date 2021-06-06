using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.greatProjects
{
	class GreatProject
	{
		[PropertyName]
		public string Name { get; set; }
		public int Start { get; set; }
		public int BuildCost { get; set; }
		public bool CanBeMoved { get; set; }
		public int StartingTier { get; set; }
		public string Type { get; set; }
		public Trigger BuildTrigger { get; set; }
		public Trigger CanUseModifiersTrigger { get; set; }
		public Trigger CanUpgradeTrigger { get; set; }
		public Trigger KeepTrigger { get; set; }
		[PropertyValue("tier_0")]
		public Tier Tier0 { get; set; }
		[PropertyValue("tier_1")]
		public Tier Tier1 { get; set; }
		[PropertyValue("tier_2")]
		public Tier Tier2 { get; set; }
		[PropertyValue("tier_3")]
		public Tier Tier3 { get; set; }

	}

	class Tier
	{
		public Time UpgradeTime { get; set; }
		public Factor CostToUpgrade { get; set; }
		public IReadOnlyDictionary<string, string> ProvinceModifiers { get; set; }
		public IReadOnlyDictionary<string, string> AreaModifier { get; set; }
		public IReadOnlyDictionary<string, string> CountryModifiers { get; set; }
	}

	class Factor
	{
		[PropertyValue("factor")]
		public int Value { get; set; }
	}

	class Time
	{
		public int Months { get; set; }
	}
}
