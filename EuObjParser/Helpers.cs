using EuObjParser.Attributes;
using EuObjParser.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EuObjParser
{
	static class Helpers
	{
		public static BonusDisplayType GetDisplayType(Bonus bonus)
		{
			var enumValueName = Enum.GetName(typeof(Bonus), bonus);

			var valueType = typeof(Bonus).GetMember(enumValueName)
				.Single()
				.GetCustomAttributes(typeof(EuValueTypeAttribute), false)
				.Select(x => (EuValueTypeAttribute)x)
				.SingleOrDefault();
			return valueType.Type;
		}

		public static string FormatCountryIdeaName(string countryIdeaName)
		{
			countryIdeaName = Regex.Replace(countryIdeaName, "^[A-Z][0-9]{2}_", "");
			countryIdeaName = string.Join(" ", countryIdeaName.Split("_"));
			return countryIdeaName.Substring(0, 1).ToUpper() + countryIdeaName.Substring(1).ToLower();
		}

		public static string DisplayValue(Enums.Bonus type, string value)
		{
			if (value == null)
			{
				return null;
			}
			var enumValueName = Enum.GetName(typeof(Bonus), type);

			var valueType = typeof(Bonus).GetMember(enumValueName)
				.Single()
				.GetCustomAttributes(typeof(EuValueTypeAttribute), false)
				.Select(x => (EuValueTypeAttribute)x)
				.SingleOrDefault();
			var colonialNation = value.Contains("/ColonialNation");
			switch (valueType.Type)
			{
				case BonusDisplayType.None:
					return value;
				case BonusDisplayType.Percentage:
					if (colonialNation)
					{
						var splitValue1 = value.Split('/');
						return (decimal.Parse(splitValue1[0]) * 100).ToString("0.00") + "%" + " / Colonial nation";
					}
					if (value.Contains("/"))
					{
						return string.Join("/", value.Split("/").Select(x => (decimal.Parse(x) * 100).ToString("0.00") + "%"));
					}
					return (decimal.Parse(value) * 100).ToString("0.00") + "%";
				case BonusDisplayType.TwoDp:
					if (value.Contains("/"))
					{
						return string.Join("/", value.Split("/").Select(x => (decimal.Parse(x)).ToString("0.00")));
					}
					return (decimal.Parse(value)).ToString("0.00");
				case BonusDisplayType.OneDp:
					if (value.Contains("/"))
					{
						return string.Join("/", value.Split("/").Select(x => (decimal.Parse(x)).ToString("0.0")));
					}
					return (decimal.Parse(value)).ToString("0.0");
				case BonusDisplayType.ZeroDp:
					if (value.Contains("/"))
					{
						return string.Join("/", value.Split("/").Select(x => (decimal.Parse(x)).ToString("0")));
					}
					return (decimal.Parse(value)).ToString("0");
				case BonusDisplayType.Thousand:
					if (value.Contains("/"))
					{
						return string.Join("/", value.Split("/").Select(x => (decimal.Parse(x) * 1000).ToString("N")));
					}
					return (decimal.Parse(value) * 1000).ToString("N");
				case BonusDisplayType.YesNo:
					return value == "yes" ? "Yes" : "No";
				case BonusDisplayType.Cb:
					return value;
				case BonusDisplayType.Decision:
					return value;
				case BonusDisplayType.Estate:
					return value;
				case BonusDisplayType.ValuePerColonialNation:
					var splitValue2 = value.Split('/');
					return (decimal.Parse(splitValue2[0]) * 100).ToString("0.00") + "% / Colonial nation";
			}
			return null;
		}

		public static T GetEnum<T>(string euName)
		{
			return (T)typeof(T).GetFields().Single(x =>
			{
				var euKey = (EuKeyAttribute)x.GetCustomAttributes(typeof(EuKeyAttribute), false).FirstOrDefault();
				return euKey?.Name == euName;
			}).GetValue(null);
		}

		public static object GetEnum(Type enumType, string euName)
		{
			return enumType.GetFields().Single(x =>
			{
				var euKey = (EuKeyAttribute)x.GetCustomAttributes(typeof(EuKeyAttribute), false).FirstOrDefault();
				return euKey?.Name?.ToLower() == euName.ToLower();
			}).GetValue(null);
		}

		public static string GetName<T>(T enumVal)
		{
			var enumValueName = Enum.GetName(typeof(T), enumVal);
			return typeof(T).GetMember(enumValueName)
				.Single()
				.GetCustomAttributes(typeof(DisplayAttribute), false)
				.Select(x => (DisplayAttribute)x)
				.SingleOrDefault()?.GetName() ?? enumValueName;
		}

		private static Dictionary<EuIdeaGroupCategory, List<IdeaGroup>> _allButMap = new Dictionary<EuIdeaGroupCategory, List<IdeaGroup>>
		{
			[EuIdeaGroupCategory.NotShiaIbadiHussite] = new List<IdeaGroup> { IdeaGroup.Shia, IdeaGroup.Ibadi, IdeaGroup.Hussite }
		};

		private static Dictionary<EuIdeaGroupCategory, List<IdeaGroup>> _map = new Dictionary<EuIdeaGroupCategory, List<IdeaGroup>>
		{
			[EuIdeaGroupCategory.Army] = new List<IdeaGroup> { IdeaGroup.StandingArmy, IdeaGroup.Conscription, IdeaGroup.MercenaryArmy },
			[EuIdeaGroupCategory.Centralisation] = new List<IdeaGroup> { IdeaGroup.Centralism, IdeaGroup.Decentralism},
			[EuIdeaGroupCategory.Damage] = new List<IdeaGroup> { IdeaGroup.Fire, IdeaGroup.Shock},
			[EuIdeaGroupCategory.General] = new List<IdeaGroup> {IdeaGroup.Naval, IdeaGroup.Diplomatic, IdeaGroup.Aristocracy, IdeaGroup.Plutocracy, IdeaGroup.Administrative, IdeaGroup.Assimilation, IdeaGroup.ColonialEmpire, IdeaGroup.Defensive, IdeaGroup.Development, IdeaGroup.Dictatorship, IdeaGroup.Dynastic, IdeaGroup.Economic, IdeaGroup.Espionage, IdeaGroup.Expansion, IdeaGroup.Exploration, IdeaGroup.FleetBase, IdeaGroup.Fortress, IdeaGroup.GeneralStaff, IdeaGroup.Health, IdeaGroup.Humanist, IdeaGroup.Influence, IdeaGroup.Innovativeness, IdeaGroup.Judicial, IdeaGroup.Maritime, IdeaGroup.Militarism, IdeaGroup.Nationalism, IdeaGroup.Offensive, IdeaGroup.Propaganda, IdeaGroup.Quality, IdeaGroup.Quantity, IdeaGroup.Society, IdeaGroup.StateAdministration, IdeaGroup.Tactics, IdeaGroup.Trade, IdeaGroup.WarProduction, IdeaGroup.WeaponQuality},
			[EuIdeaGroupCategory.Government] = new List<IdeaGroup> { IdeaGroup.Theocracy, IdeaGroup.Horde, IdeaGroup.Monarchy, IdeaGroup.Republic},
			[EuIdeaGroupCategory.Imperial] = new List<IdeaGroup> { IdeaGroup.Imperial, IdeaGroup.ImperialAmbition },
			[EuIdeaGroupCategory.Muslim] = new List<IdeaGroup> { IdeaGroup.Shia, IdeaGroup.Sunni, IdeaGroup.Ibadi},
			[EuIdeaGroupCategory.NonRepublic] = new List<IdeaGroup> { IdeaGroup.Theocracy, IdeaGroup.Horde, IdeaGroup.Monarchy},
			[EuIdeaGroupCategory.Religion] = new List<IdeaGroup> {
				IdeaGroup.Religious,
				IdeaGroup.Catholic,
				IdeaGroup.Protestant,
				IdeaGroup.Reformed,
				IdeaGroup.Orthodox,
				IdeaGroup.Sunni,
				IdeaGroup.Norse,
				IdeaGroup.Buddhist,
				IdeaGroup.Confucian,
				IdeaGroup.Hindu,
				IdeaGroup.Tengri,
				IdeaGroup.Coptic,
				IdeaGroup.Hellanistic,
				IdeaGroup.Slav,
				IdeaGroup.Jewish,
				IdeaGroup.Shinto,
				IdeaGroup.Cathar,
				IdeaGroup.Suomi,
				IdeaGroup.Romuva,
				IdeaGroup.Animist,
				IdeaGroup.Fetishist,
				IdeaGroup.Zoroastrianism,
				IdeaGroup.Manichean,
				IdeaGroup.Anglican,
				IdeaGroup.Mesoamerican,
				IdeaGroup.Inti,
				IdeaGroup.Totemism,
				IdeaGroup.Nahuatl,
				IdeaGroup.Shia,
				IdeaGroup.Ibadi,
				IdeaGroup.Hussite
			},
			[EuIdeaGroupCategory.Ship] = new List<IdeaGroup> { IdeaGroup.HeavyShip, IdeaGroup.LightShip, IdeaGroup.Galley},
			[EuIdeaGroupCategory.NonLightShip] = new List<IdeaGroup> { IdeaGroup.HeavyShip, IdeaGroup.Galley},
			[EuIdeaGroupCategory.NonHeavyShip] = new List<IdeaGroup> { IdeaGroup.LightShip, IdeaGroup.Galley},
			[EuIdeaGroupCategory.NonMerc] = new List<IdeaGroup> { IdeaGroup.StandingArmy, IdeaGroup.Conscription},
		};

		public static EuIdeaGroupCategory GetExclusiveCategory(IdeaGroup group)
		{
			for(var i = 0; i<8; i++)
			{
				var thisOne = _map[(EuIdeaGroupCategory)i];
				if (thisOne.Contains(group))
				{
					return (EuIdeaGroupCategory)i;
				}
			}
			throw new Exception(group + " was not found");
		}

		//public static EuIdeaGroupCategory GetCompleteCategory(List<string> groups)
		//{
		//	var kvs = _map.Select(x => new { x.Value, x.Key });
			
		//	groups.Sort();
		//	foreach(var kv in kvs)
		//	{
		//		kv.Value.Sort();
		//	}
			
		//	var matching = kvs.Where(x => Enumerable.SequenceEqual(x.Value, groups)).ToList();
			
			

		//	if(matching.Count != 1)
		//	{
		//		var allButKvs = _allButMap.Select(x => new { x.Value, x.Key });
		//		foreach (var kv in allButKvs)
		//		{
		//			kv.Value.Sort();
		//		}
				
		//		var ideaGroups = Enum.GetValues(typeof(IdeaGroup));
		//		var diff = new List<IdeaGroup>();
		//		foreach(IdeaGroup group in ideaGroups)
		//		{
		//			if (!groups.Contains(group))
		//			{
		//				diff.Add(group);
		//			}
		//		}
		//		diff.Sort();
		//		var allButMatching = allButKvs.Where(x => Enumerable.SequenceEqual(x.Value, diff)).ToList();
		//		if(allButMatching.Count == 1)
		//		{
		//			return allButMatching.Single().Key;
		//		} else
		//		{
		//			if(diff.Count == 0)
		//			{
		//				return EuIdeaGroupCategory.Any;
		//			}
		//		}
		//	}

		//	return matching.SingleOrDefault()?.Key ??
		//		(groups.Any(x => _map[EuIdeaGroupCategory.Religion].Contains(x))
		//			? EuIdeaGroupCategory.SomeReligion
		//			: (EuIdeaGroupCategory?)null) ?? (EuIdeaGroupCategory)1000;
		//}

		public static string GetImageUrl<T>(T thing)
		{
			InitImageMappings();
			if(thing is MonarchPower mp)
			{
				return "Icons/" + MonarchPowerMappings[GetName(mp)];
			}
			if(thing is Bonus b)
			{
				var mappingExists = BonusMappings.ContainsKey(GetName(b));
				return "Icons/Bonuses/" + (mappingExists
					? BonusMappings[GetName(b)]
					: DefaultNameMap(GetName(b)));
			}
			return null;
		}

		private static string DefaultNameMap(string s)
		{
			var replaced = string.Join("_", s.Split(" "));
			var cased = replaced[0].ToString().ToUpper() + replaced.Substring(1).ToLower() + ".png";
			return cased;
		}

		private static Dictionary<string, string> MonarchPowerMappings { get; set; }
		private static Dictionary<string, string> BonusMappings { get; set; }

		private static void InitImageMappings()
		{
			if(BonusMappings == null)
			{
				var str = @"{monarchPowerMappings : {
    ""ADM"": ""Administrative_power.png"",

	""DIP"": ""Diplomatic_power.png"",
    ""MIL"": 'Military_power.png',
},

		bonusMappings : {
	""Interest"": ""Interest_per_annum.png"",
	""Goods Produced"": ""Goods_produced_modifier.png"",
	""Reduce inflation cost reduction"" : ""Reduce_inflation_cost.png"",
	""Institution spread from true faith"": ""Institution_spread_in_true_faith_provinces.png"",
	""Global institution spread"": ""Institution_spread.png"",
	""Global institution growth"": ""Institution_growth.png"",
	""Backrow artillery damage"": ""Artillery_damage_from_back_row.png"",
	""Hostile attrition"": ""Attrition_for_enemies.png"",
	""Fort maintenance modifier"": ""Fort_maintenance.png"",
	""Yearly papal influence"": ""Papal_influence.png"",
	""Church power modifier"": ""Church_power.png"",
	""Monthly fervor increase"": ""Monthly_fervor.png"",
	""Tolerance of true faith"": ""Tolerance_of_the_true_faith.png"",
	""Defensiveness"": ""Fort_defense.png"",
	""Fire damage"": ""Land_fire_damage.png"",
	""National manpower"": ""Manpower.png"",
	""Manpower per age"": ""Manpower.png"",
	""National sailors"": ""Sailors.png"",
	""Sailors per age"": ""Sailors.png"",
	""Diplomats"": ""Diplomat.png"",
	""Reduced stability impact from diplo"": ""Reduced_stab_impacts.png"",
	""Improve relations modifier"": ""Improve_relations.png"",
	""Yearly absolutism"": ""Absolutism.png"",
	""Diplo tech cost modifier"": ""Diplomatic_technology_cost.png"",
	""Admin tech cost modifier"": ""Administrative_technology_cost.png"",
	""Military tech cost modifier"": ""Military_technology_cost.png"",
	""Add CB"": ""Casus_belli.png"",
	""Relation with heretics"": ""Opinion_of_heretics.png"",
	""Land attrition"": ""Attrition.png"",
	""Number of accepted cultures"": ""Max_promoted_cultures.png"",
	""Tolerance of heathens"": ""Tolerance_heathen.png"",
	""Tolerance of heretics"": ""Tolerance_heretic.png"",
	""Autonomy modifier"": ""Autonomy.png"",
	""Global supply limit modifier"": ""Supply_limit.png"",
	""Trade range modifier"": ""Trade_range.png"",
	""Global trade power"": ""Trade_power.png"",
	""State maintenance modifier"": ""State_maintenance.png"",
	""Tariffs"": ""Local_tariffs.png"",
	""Reinforce cost modifier"": ""Reinforce_cost.png"",
	""Marine fraction"": ""Marines_force_limit.png"",
	""Siege blockade progress"": ""Ab_siege_blockades.png"",
	""Morale bonus from 5 cultures"": ""Morale_of_armies.png"",
	""Enforce religion cost"": ""Cost_of_enforcing_religion_through_war.png"",
	""Build cost in subject nation"": ""Construction_cost.png"",
	""Build cost in colonial nation"": ""Construction_cost.png"",
	""Sailor maintenance modifier"": ""Sailor_maintenance.png"",
	""Add naval force limit per age"": ""Naval_force_limit.png"",
	""Extra navy tradition from galleys"": ""Yearly_navy_tradition.png"",
	""Extra navy tradition from light ships"": ""Yearly_navy_tradition.png"",
	""Extra navy tradition from heavy ships"": ""Yearly_navy_tradition.png"",
	""Devepment cost for provinces over 25 dev"": ""Development_cost.png"",
	""Reduced dev cost malus from nation size"": ""Development_cost.png"",
	""Inflationreduction"": ""Yearly_inflation_reduction.png"",
	""Extra manpower at religious war"": ""Triple_manpower_increase_in_religious_wars.png"",
	""Yearly army professionalism"": ""Army_professionalism.png"",
	""Estate interaction"": ""Estates.png"",
	""Global garrison growth"": ""Garrison_size.png"",
	""Prestige per development from conversion"": ""Prestige_per_development_from_missionary.png"",
	""Monthly militarized state"": ""Militarization_of_state.png"",
	""Claim duration"": ""Claim.png"",
	""Claim colonies"": ""Claim.png"",
	""Core decay on your own"": ""Claim.png"",
	""Add manadate/IA per age"": ""Imperial_authority.png"",
	""Local development cost"": ""Development_cost.png"",
	""Army tradition from battle"": ""Army_tradition.png"",
	""Merchant trade power"": ""Merchants.png"",
	""Brahmins hindu loyalty modifier"": ""Brahmins_loyalty_modifier.png"",
	""Brahmins hindu influence modifier"": ""Brahmins_influence_modifier.png"",
	""Brahmins muslim influence modifier"": ""Brahmins_influence_modifier.png"",
	""Brahmins muslim loyalty modifier"": ""Brahmins_loyalty_modifier.png"",
}}";

				var obj = JObject.Parse(str);
				MonarchPowerMappings = obj["monarchPowerMappings"].Value<JObject>().Properties().ToDictionary(x => x.Name, x => x.Value.Value<string>());
				BonusMappings = obj["bonusMappings"].Value<JObject>().Properties().ToDictionary(x => x.Name, x => x.Value.Value<string>());
			}
			
		}
	}
}
