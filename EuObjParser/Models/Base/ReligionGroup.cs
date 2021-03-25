using EuObjParser.Attributes;
using EuObjParser.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuObjParser.Models.Base
{
	class ReligionGroup
	{
		[EuObjPropName]
		public Enums.ReligionGroup Name { get; set; }
		[EuPropName("defender_of_faith")]
		public bool DefenderOfFaith { get; set; }
		[EuPropName("can_form_personal_unions")]
		public bool CanFormPersonalUnions { get; set; }
		[EuPropName("flags_with_emblem_percentage")]
		public int FlagsWithEmblemPercentage { get; set; }
		[EuPropName("center_of_religion")]
		public int CenterOfReligion { get; set; }
		[EuPropName("crusade_name")]
		public string CrusadeName { get; set; }
		[EuPropName("harmonized_modifier")]
		public string HarmonizedModifier { get; set; }
		[EuPropNameRemaining("flag_emblem_index_range")]
		public List<Religion> Religions { get; set; }
	}

	class Religion
	{
		[EuObjPropName]
		public Enums.Religion Name { get; set; }
		[EuPropName("color")]
		public Color Color { get; set; }
		[EuPropName("icon")]
		public int Icon { get; set; }
		[EuPropName("country")]
		public List<Bonus> Country { get; set; }
		[EuPropName("country_as_secondary")]
		public List<Bonus> SecondaryCountry { get; set; }
		[EuPropName("province")]
		public List<Bonus> Province { get; set; }
		[EuObjList("heretic")]
		public List<string> Heretic { get; set; }
		[EuPropName("personal_deity")]
		public bool PersonalDeity { get; set; }
		[EuPropName("uses_anglican_power")]
		public bool UsesAnglicanPower { get; set; }
		[EuPropName("aspects_name")]
		public string AspectsName { get; set; }
		[EuObjList("aspects")]
		public List<string> Aspects { get; set; }
		[EuObjList("allowed_conversion")]
		public List<string> AllowedConversion { get; set; }
		[EuObjList("allowed_center_conversion")]
		public List<string> AllowedCenterConversion { get; set; }
		[EuObjList("blessings")]
		public List<Enums.ChurchAspect> Blessings { get; set; }
		[EuPropName("holy_sites")]
		public ReligionHolySites HolySites { get; set; }
		[EuPropName("date")]
		public string Date { get; set; }
		[EuPropName("hre_religion")]
		public bool HreReligion { get; set; }
		[EuPropName("hre_heretic_religion")]
		public bool HreHereticReligion { get; set; }
		[EuPropName("fervor")]
		public bool Fervor { get; set; }
	}

	class ReligionHolySites
	{
		[EuObjPropValue]
		[JsonIgnore]
		public string Value { get; set; }

		public List<int> ProvinceIds => Value?.Split(" ").Select(int.Parse)?.ToList();
	}

	//class ReligionHeretic
	//{
	//	public List<string> Value { get; set; }
	//}
}
