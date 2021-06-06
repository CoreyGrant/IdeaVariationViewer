using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Models.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EuObjParser.AAParser
{
	class ClauzwitzJsonLoader
	{
		public IReadOnlyDictionary<string, string> LoadLocalizations(string folder)
		{
			return LoadObject<Dictionary<string, string>>(Path.Combine(folder, "localisations.json"));
		}

		public List<Country> LoadCountries(string folder, string[] except = null)
		{
			var fileNames = Directory.GetFiles(Path.Combine(folder, "common", "countries"));
			if (except != null)
			{
				fileNames = fileNames.Where(x => !except.Any(y => x.EndsWith(y + ".json"))).ToArray();
			}
			var list = new List<Country>();
			foreach (var fileName in fileNames)
			{
				var country = LoadObject<Country>(fileName);
				list.Add(country);
			}
			return list;
		}

		public List<CountryHistory> LoadHistoryCountries(string folder, string[] except = null)
		{
			var fileNames = Directory.GetFiles(Path.Combine(folder, "history", "countries"));
			if (except != null)
			{
				fileNames = fileNames.Where(x => !except.Any(y => x.EndsWith(y + ".json"))).ToArray();
			}
			var list = new List<CountryHistory>();
			foreach (var fileName in fileNames)
			{
				var countryHistory = LoadObject<CountryHistory>(fileName);
				list.Add(countryHistory);
			}
			return list;
		}

		public List<KeyValuePair<string, string>> LoadCountryTags(string folder, string[] except = null)
		{
			var fileNames = Directory.GetFiles(Path.Combine(folder, "common", "country_tags"));
			if (except != null)
			{
				fileNames = fileNames.Where(x => !except.Any(y => x.EndsWith(y + ".json"))).ToArray();
			}
			var dict = new Dictionary<string, string>();
			foreach (var fileName in fileNames)
			{
				var countryTagFile = LoadObject(fileName);
				var props = countryTagFile.Properties().Where(x => x.Name != "_filename");
				foreach (var tag in props)
				{
					dict[tag.Name] = tag.Value.ToString().Replace("\"", "");
				}
			}
			return dict.ToList();
		}

		public List<IdeaGroup> LoadIdeas(string folder, string[] except = null)
		{
			var fileNames = Directory.GetFiles(Path.Combine(folder, "common", "ideas"));
			if (except != null)
			{
				fileNames = fileNames.Where(x => !except.Any(y => x.EndsWith(y + ".json"))).ToArray();
			}
			var ideaGroups = new List<IdeaGroup>();
			foreach (var fileName in fileNames)
			{
				var obj = LoadObject(fileName);

				var ideaGroupProps = obj.Properties().Where(x => x.Name != "_filename");
				foreach (var ideaGroupProp in ideaGroupProps)
				{
					var ideaGroupObj = (JObject)ideaGroupProp.Value;
					var trigger = LoadTrigger((JObject)ideaGroupObj["trigger"]);
					var ideaProps = ideaGroupObj.Properties()
						.Where(x => x.Name != "trigger"
							&& x.Name != "category"
							&& x.Name != "free" && x.Name != "important");
					var ideaGroup = new IdeaGroup
					{
						Name = ideaGroupProp.Name,
						Category = ideaGroupObj.Value<string>("category"),
						Ideas = ideaProps.Select(x => new Idea
						{
							Name = x.Name,
							Bonuses = ((JObject)x.Value).Properties().ToDictionary(y => y.Name, y => y.Value.Value<string>())
						}).ToList(),
						Trigger = trigger,
					};
					ideaGroups.Add(ideaGroup);
				}
			}
			return ideaGroups;
		}

		public List<Policy> LoadPolicies(string folder, string[] except = null)
		{
			var fileNames = Directory.GetFiles(Path.Combine(folder, "common", "policies"));
			if (except != null)
			{
				fileNames = fileNames.Where(x => !except.Any(y => x.EndsWith(y + ".json"))).ToArray();
			}
			var policies = new List<Policy>();
			foreach (var fileName in fileNames)
			{
				var obj = LoadObject(fileName);
				var props = obj.Properties().Where(x => x.Name != "_filename");
				foreach (var prop in props)
				{
					var policyObj = (JObject)prop.Value;
					var bonusProps = policyObj.Properties().Where(x => x.Name != "monarch_power" && x.Name != "potential" && x.Name != "allow");

					var policy = new Policy
					{
						Name = prop.Name,
						MonarchPower = policyObj.Value<string>("monarch_power"),
						Potential = LoadTrigger((JObject)policyObj["potential"]),
						Allow = LoadTrigger((JObject)policyObj["allow"]),
						Bonuses = bonusProps.ToDictionary(x => x.Name, x => x.Value.Value<string>())
					};

					policies.Add(policy);
				}
			}
			return policies;
		}

		private readonly string[] ReligionGroupPropNameIgnore = new[] { "defender_of_faith", "can_form_personal_unions", "center_of_religion", "flags_with_emblem_percentage", "flag_emblem_index_range", "harmonized_modifier", "crusade_name", "religious_schools", "ai_will_propagate_through_trade" };

		public List<ReligionGroup> LoadReligions(string folder, string[] except = null)
		{
			var fileNames = Directory.GetFiles(Path.Combine(folder, "common", "religions"));
			if (except != null)
			{
				fileNames = fileNames.Where(x => !except.Any(y => x.EndsWith(y + ".json"))).ToArray();
			}
			var religionGroups = new List<ReligionGroup>();
			foreach (var fileName in fileNames)
			{
				var obj = LoadObject(fileName);

				var religionGroupProps = obj.Properties().Where(x => x.Name != "_filename");
				foreach (var religionGroupProp in religionGroupProps)
				{
					var religionGroupValue = (JObject)religionGroupProp.Value;
					var religionProps = religionGroupValue.Properties()
						.Where(x => !ReligionGroupPropNameIgnore.Contains(x.Name));
					var religions = religionProps.Select(x => 
					{
						var religionValue = (JObject)x.Value;
						return new Religion
						{
							Name = x.Name,
							Color = ((JArray)religionValue["color"]).Select(x => x.Value<string>()).ToList(),
							Country = ((JObject)religionValue["country"]).Properties().ToDictionary(x => x.Name, x => x.Value.Value<string>()),
							CountryAsSecondary = ((JObject)religionValue["country_as_secondary"])?.Properties()?.ToDictionary(x => x.Name, x => x.Value.Value<string>()),
							Province = ((JObject)religionValue["province"])?.Properties()?.ToDictionary(x => x.Name, x => x.Value.Value<string>()),
							Icon = religionValue.Value<string>("icon")
						};
					}).ToList();
					var religionGroup = new ReligionGroup
					{
						Name = religionGroupProp.Name,
						DefenderOfFaith = religionGroupValue.Value<string>("defender_of_faith"),
						CanFormPersonalUnions = religionGroupValue.Value<string>("can_form_personal_unions"),
						CenterOfReligion = religionGroupValue.Value<string>("center_of_religion"),
						Religions = religions,
					};
					religionGroups.Add(religionGroup);
				}
			}
			return religionGroups;
		}

		public List<CultureGroup> LoadCultures(string folder, string[] except = null)
		{
			var fileNames = Directory.GetFiles(Path.Combine(folder, "common", "cultures"));
			if (except != null)
			{
				fileNames = fileNames.Where(x => !except.Any(y => x.EndsWith(y + ".json"))).ToArray();
			}
			var cultureGroups = new List<CultureGroup>();
			foreach (var fileName in fileNames)
			{
				var obj = LoadObject(fileName);

				var cultureGroupProps = obj.Properties().Where(x => x.Name != "_filename");
				foreach (var cultureGroupProp in cultureGroupProps)
				{
					var cultureProps = ((JObject)cultureGroupProp.Value).Properties().Where(x => x.Name != "graphical_culture" && x.Name != "second_graphical_culture");
					var cultureGroup = new CultureGroup
					{
						Name = cultureGroupProp.Name,
						Cultures = cultureProps.Select(x => new Culture
						{
							Name = x.Name,
							Primary = x.Value.Value<string>("primary"),
						}).ToList()
					};
					cultureGroups.Add(cultureGroup);
				}
			}
			return cultureGroups;
		}

		//public JArray LoadGreatProjects(string folder)
		//{

		//}

		private JObject LoadObject(string fileName)
		{
			using (var file = File.OpenText(fileName))
			using (var reader = new JsonTextReader(file))
			{
				return (JObject)JObject.ReadFrom(reader);
			}
		}

		private T LoadObject<T>(string fileName)
		{
			var fileText = File.ReadAllText(fileName);
			return JsonConvert.DeserializeObject<T>(fileText);
		}

		private Trigger LoadTrigger(JObject triggerObj)
		{
			if(triggerObj == null) 
			{
				return null; 
			}
			var trigger = new Trigger
			{
				Conditions = new List<TriggerCondition>(),
				ConditionSets = new List<TriggerConditionSet>(),
			};
			var props = triggerObj.Properties();
			foreach (var prop in props)
			{
				var upperName = prop.Name.ToUpper();
				if (upperName == "OR" || upperName == "AND" || upperName == "NOT" || upperName == "HIDDEN_TRIGGER" || upperName == "CALC_TRUE_IF")
				{
					trigger.ConditionSets.Add(LoadTriggerConditionSet(prop.Value,
						prop.Name == "OR",
						prop.Name == "NOT"));
				}
				else
				{
					if (prop.Value is JArray arrayVal)
					{
						foreach (var val in arrayVal)
						{
							trigger.Conditions.Add(new TriggerCondition
							{
								Name = prop.Name,
								Value = val.Value<string>()
							});
						}
					}
					else
					{
						//TODO: Fix this properly, we need to take this into account
						if(prop.Name == "capital_scope") { continue; }
						trigger.Conditions.Add(new TriggerCondition
						{
							Name = prop.Name,
							Value = prop.Value.Value<string>(),
						});
					}
				}
			}
			return trigger;
		}

		private TriggerConditionSet LoadTriggerConditionSet(
			JToken csTok,
			bool composeOr,
			bool modifierNot)
		{
			var props = csTok is JObject csObj 
				? csObj.Properties()
				: ((JArray)csTok).SelectMany(x => ((JObject)x).Properties());
			var cs = new TriggerConditionSet
			{
				ComposeOr = composeOr,
				ModifierNot = modifierNot,
				Conditions = new List<TriggerCondition>(),
				ConditionSets = new List<TriggerConditionSet>()
			};
			foreach (var prop in props)
			{
				var upperName = prop.Name.ToUpper();
				if (upperName == "OR" || upperName == "AND" || upperName == "NOT" || upperName == "HIDDEN_TRIGGER" || upperName == "CALC_TRUE_IF")
				{
					cs.ConditionSets.Add(LoadTriggerConditionSet(
						prop.Value,
						prop.Name == "OR",
						prop.Name == "NOT"));
				}
				else
				{
					if (prop.Value is JArray arrayVal)
					{
						foreach (var val in arrayVal)
						{
							cs.Conditions.Add(new TriggerCondition
							{
								Name = prop.Name,
								Value = val.Value<string>()
							});
						}
					}
					else
					{
						if (prop.Name == "capital_scope") { continue; }
						cs.Conditions.Add(new TriggerCondition
						{
							Name = prop.Name,
							Value = prop.Value.ToString(),
						});
					}
				}
			}
			return cs;
		}
	}
}
