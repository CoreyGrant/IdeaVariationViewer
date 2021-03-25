//using EuObjParser.Models;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace EuObjParser.Parsing
//{
//	class Parser
//	{
//		public List<Models.IdeaGroup> ParseIdeaGroups(string s)
//		{
//			var jObject = JObject.Parse(s);
//			var props = jObject.Properties();
//			var ideaGroups = new List<Models.IdeaGroup>();
//			foreach (var prop in props)
//			{
//				var name = prop.Name;
//				var ideaGroup = new Models.IdeaGroup
//				{
//					Type = Helpers.GetEnum<Enums.IdeaGroup>(name),
//					Ideas = new List<Idea>(),
//				};
//				var value = prop.Value.Value<JObject>();
//				var order = 0;
//				foreach (var prop2 in value.Properties())
//				{
//					var name2 = prop2.Name;
//					var value2 = prop2.Value;
//					if (name2 == "category")
//					{
//						ideaGroup.Category = Helpers.GetEnum<Enums.MonarchPower>(value2.Value<string>());
//					}
//					else if (name2 == "trigger")
//					{
//						var ideaGroupTrigger = new IdeaGroupTrigger
//						{

//						};
//						var trigger = value2.Value<JObject>();
//						if (!trigger.ContainsKey("government_rank")
//							&& trigger.ContainsKey("NOT")
//							&& trigger["NOT"].Value<JObject>().ContainsKey("OR"))
//						{
//							var listObj = trigger["NOT"]["OR"].Value<JObject>();
//							var religions = ParseListObj(listObj, "religion");
//							var religionGroups = ParseListObj(listObj, "religion_group");
//							var hasIdeaGroups = ParseListObj(listObj, "has_idea_group");
//							ideaGroupTrigger.NotAnyIdeas = hasIdeaGroups.Select(Helpers.GetEnum<Enums.IdeaGroup>).ToList();
//							ideaGroupTrigger.NotAnyReligions = religions;
//							ideaGroupTrigger.NotAnyReligionGroups = religionGroups;
//						}
//						if (trigger.ContainsKey("government_rank"))
//						{
//							ideaGroupTrigger.GovernmentRank = new IdeaGroupTriggerGovernmentRank { Emperor = false };
//						}
//						if (trigger.ContainsKey("OR")
//							&& trigger["OR"].Value<JObject>().ContainsKey("government_rank"))
//						{
//							ideaGroupTrigger.GovernmentRank = new IdeaGroupTriggerGovernmentRank { Emperor = true };
//						}
//						if (trigger.ContainsKey("NOT"))
//						{
//							var listObj = trigger["NOT"].Value<JObject>();
//							var hasIdeaGroups = ParseListObj(listObj, "has_idea_group");
//							ideaGroupTrigger.NotAnyIdeas = hasIdeaGroups.Select(Helpers.GetEnum<Enums.IdeaGroup>).ToList();
//						}
//						if (trigger.ContainsKey("primatives"))
//						{
//							ideaGroupTrigger.Primatives = false;
//						}
//						if (trigger.Properties().Any(x => x.Name.StartsWith("religion")))
//						{
//							var religions = ParseListObj(trigger, "religion");
//							ideaGroupTrigger.Religions = religions;
//						}
//						if (trigger.Properties().Any(x => x.Name.StartsWith("religion_group")))
//						{
//							var religionGroups = ParseListObj(trigger, "religion_group");
//							ideaGroupTrigger.ReligionGroups = religionGroups;
//						}
//						ideaGroup.Trigger = ideaGroupTrigger;
//					}
//					else if (name2 == "important")
//					{
//						ideaGroup.Important = true;
//					}
//					else if (name2 != "ai_will_do")
//					{
//						var bonusProps = value2.Value<JObject>().Properties();
//						ideaGroup.Ideas.Add(new Idea
//						{
//							Name = name2,
//							Order = order++,
//							Bonuses = bonusProps.Select(x => new Models.Bonus
//							{
//								Type = Helpers.GetEnum<Enums.Bonus>(x.Name),
//								Value = x.Value.Value<string>(),
//							}).ToList()
//						});
//					}
//				}
//				ideaGroup.Ideas.Sort((x, y) => x.Name == "bonus"
//					? -1
//					: (x.Order < y.Order ? -1 : 1));
//				ideaGroups.Add(ideaGroup);
//			}
//			ideaGroups.Sort((x, y) => (int)x.Type < (int)y.Type ? -1 : 1);
//			return ideaGroups;
//		}

//		public List<Policy> ParsePolicies(string s)
//		{
//			var jObject = JObject.Parse(s);
//			var props = jObject.Properties();
//			var policies = new List<Policy>();
//			foreach (var prop in props)
//			{
//				var name = prop.Name;
//				var policy = new Policy { Name = name, Bonuses = new List<Models.Bonus>() };
//				var value = prop.Value.Value<JObject>();
//				foreach (var prop2 in value.Properties())
//				{
//					var name2 = prop2.Name;
//					var value2 = prop2.Value;
//					if (name2 == "monarch_power")
//					{
//						switch (value2.Value<string>())
//						{
//							case "ADM":
//								policy.MonarchPower = Enums.MonarchPower.Adm;
//								break;
//							case "DIP":
//								policy.MonarchPower = Enums.MonarchPower.Dip;
//								break;
//							case "MIL":
//								policy.MonarchPower = Enums.MonarchPower.Mil;
//								break;
//						}
//					}
//					else
//					if (name2 == "potential")
//					{
//						var orHas = new List<string>();
//						if (value2.Value<JObject>().ContainsKey("OR"))
//						{
//							orHas = ParseListObj(value2["OR"].Value<JObject>(), "has_idea_group");
//						}
//						var hasIdeaGroups = ParseListObj(value2.Value<JObject>(), "has_idea_group");
//						policy.Potential = new PolicyPotential
//						{
//							Has = hasIdeaGroups.Select(Helpers.GetEnum<Enums.IdeaGroup>).ToList(),
//							HasAny = orHas.Select(Helpers.GetEnum<Enums.IdeaGroup>).ToList()
//						};
//					}
//					else
//					if (name2 == "allow")
//					{
//						var orHas = new List<string>();
//						if (value2.Value<JObject>().ContainsKey("OR"))
//						{
//							orHas = ParseListObj(value2["OR"].Value<JObject>(), "full_idea_group");
//						}
//						var fullIdeaGroups = ParseListObj(value2.Value<JObject>(), "full_idea_group");
//						policy.Allow = new PolicyAllow
//						{
//							Full = fullIdeaGroups.Select(Helpers.GetEnum<Enums.IdeaGroup>).ToList(),
//							FullAny = orHas.Select(Helpers.GetEnum<Enums.IdeaGroup>).ToList(),
//						};

//						if (value2.Value<JObject>().ContainsKey("NOT")
//							&& value2.Value<JObject>()["NOT"].Value<JObject>().ContainsKey("calc_true_if"))
//						{
//							var limitObj = value2["NOT"]["calc_true_if"].Value<JObject>();
//							var activePolicies = ParseListObj(limitObj, "has_active_policy");
//							var amount = int.Parse(limitObj["amount"].Value<string>());
//							policy.Allow.PolicyAllowLimit = new PolicyAllowLimit
//							{
//								Amount = amount,
//								Policies = activePolicies
//							};
//						}
//						if (value2.Value<JObject>().ContainsKey("current_age"))
//						{
//							policy.Allow.CurrentAge = value2.Value<JObject>()["current_age"].Value<string>();
//						}
//					}
//					else
//					if (name2 != "ai_will_do")
//					{
//						policy.Bonuses.Add(new Models.Bonus
//						{
//							Type = Helpers.GetEnum<Enums.Bonus>(name2),
//							Value = value2.Value<string>()
//						});
//					}
//				}
//				policies.Add(policy);
//			}
//			return CollapsePolicies(policies);
//		}

//		public List<Country> ParseGenericCountries(string genericCountries)
//		{
//			var genericCountriesProps = JObject.Parse(genericCountries).Properties();
//			var countries = new List<Country>();
//			foreach (var prop in genericCountriesProps)
//			{
//				var name = prop.Name.Replace("_ideas", "").Replace("_", " ");
//				name = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
//				var props = prop.Value.Value<JObject>().Properties().Where(x => !new[] { "trigger", "free" }.Contains(x.Name));
//				var ideas = props.Select(x => new Idea
//				{
//					Name = x.Name,
//					Bonuses = x.Value.Value<JObject>().Properties().Select(y => new Models.Bonus
//					{
//						Type = Helpers.GetEnum<Enums.Bonus>(y.Name),
//						Value = y.Value.Value<string>(),
//					}).ToList(),
//				}).ToList();
//				var country = new Country
//				{
//					Name = name,
//					Ideas = ideas
//				};
//			}
//			return countries;
//		}

//		public List<Country> ParseCountries(string tags, string ideas, string colors, string allCountries)
//		{
//			var tagsObj = JObject.Parse(tags);
//			var tagKvs = tagsObj.Properties().ToDictionary(x => x.Name, x => x.Value);
//			var colorsObj = JObject.Parse(colors);
//			var ideasObj = JObject.Parse(ideas);
//			var allCountriesObj = JObject.Parse(allCountries);
//			var countries = new List<Country>();
//			foreach (var kv in tagKvs)
//			{
//				if (!ideasObj.ContainsKey(kv.Key + "_ideas"))
//				{
//					continue;
//				}
//				var ideaObj = ideasObj[kv.Key + "_ideas"].Value<JObject>();
//				var name = kv.Value.Value<string>().Replace("countries/", "").Replace(".txt", "");
//				var country = new Country
//				{
//					Name = name,
//					Ideas = ideaObj.Properties().Where(x => !new[] { "trigger", "free" }.Contains(x.Name))
//						.Select(x => new Idea
//						{
//							Name = x.Name,
//							Bonuses = x.Value.Value<JObject>().Properties().Select(y => new Models.Bonus
//							{
//								Type = Helpers.GetEnum<Enums.Bonus>(y.Name),
//								Value = y.Value.Value<string>(),
//							}).ToList(),
//						}).ToList(),
//				};
//				if (allCountriesObj.ContainsKey(name))
//				{
//					var colorParts = allCountriesObj[name]["color"].Value<string>().Split("/").Select(int.Parse).ToList();
//					country.Colors = new List<CountryColor> { new CountryColor { Red = colorParts[0], Green = colorParts[1], Blue = colorParts[2] } };
//				}
//				if (colorsObj.ContainsKey(kv.Key))
//				{
//					var countryColours = colorsObj[kv.Key];
//					var first = countryColours["color1"].Value<string>().Split("/").Select(int.Parse).ToList();
//					var second = countryColours["color2"].Value<string>().Split("/").Select(int.Parse).ToList();
//					var third = countryColours["color3"].Value<string>().Split("/").Select(int.Parse).ToList();
//					country.Colors = new List<CountryColor>
//				{
//					new CountryColor{Red = first[0],Green = first[1], Blue =first[2] },
//					new CountryColor{Red = second[0],Green = second[1], Blue =second[2] },
//					new CountryColor{Red = third[0],Green = third[1], Blue =third[2] },
//				};
//				}
//				countries.Add(country);
//			}
//			return countries;
//		}

//		private List<Policy> CollapsePolicies(List<Policy> policies)
//		{
//			var ageList = new List<string> { "age_of_discovery", "age_of_reformation", "age_of_absolutism", "age_of_revolutions", };
//			var policiesWithCurrentAge = policies
//				.Where(x => !string.IsNullOrEmpty(x.Allow.CurrentAge));
//			var otherPolicies = policies
//				.Where(x => string.IsNullOrEmpty(x.Allow.CurrentAge));
//			var buffer = new Dictionary<string, Dictionary<int, Policy>>();
//			foreach (var policy in policiesWithCurrentAge)
//			{
//				if (!buffer.ContainsKey(policy.DisplayName))
//				{
//					buffer[policy.DisplayName] = new Dictionary<int, Policy>();
//				}
//				buffer[policy.DisplayName][ageList.IndexOf(policy.Allow.CurrentAge)] = policy;
//			}
//			var collapsedPolicies = buffer.Select(x => {
//				var basePolicy = x.Value[0];
//				return new Policy
//				{
//					Allow = new PolicyAllow
//					{
//						Full = basePolicy.Allow.Full,
//						FullAny = basePolicy.Allow.FullAny,
//						HiddenTrigger = basePolicy.Allow.HiddenTrigger,
//						PolicyAllowLimit = basePolicy.Allow.PolicyAllowLimit
//					},
//					MonarchPower = basePolicy.MonarchPower,
//					Name = basePolicy.Name,
//					Potential = basePolicy.Potential,
//					Bonuses = basePolicy.Bonuses.Select(b => new Models.Bonus
//					{
//						Type = b.Type,
//						Value = Helpers.DisplayValue(b.Type, b.Value) + "/"
//							+ Helpers.DisplayValue(x.Value[1].Bonuses.Single(bb => bb.Type == b.Type).Type, x.Value[1].Bonuses.Single(bb => bb.Type == b.Type).Value)
//							+ "/"
//							+ Helpers.DisplayValue(x.Value[2].Bonuses.Single(bb => bb.Type == b.Type).Type, x.Value[2].Bonuses.Single(bb => bb.Type == b.Type).Value)
//							+ "/"
//							+ Helpers.DisplayValue(x.Value[3].Bonuses.Single(bb => bb.Type == b.Type).Type, x.Value[3].Bonuses.Single(bb => bb.Type == b.Type).Value),
//						AutoDisplay = true,
//					}).ToList(),
//				};
//			}).ToList();
//			return otherPolicies.Concat(collapsedPolicies).ToList();
//		}

//		private List<string> ParseListObj(JObject obj, string key)
//		{
//			var allProps = obj.Properties().Where(x => x.Name.StartsWith(key));
//			return allProps.Values<string>().ToList();
//		}
//	}
//}
