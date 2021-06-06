//using EuObjParser.AAParser;
//using EuObjParser.Config;
//using EuObjParser.Models;
//using EuObjParser.Models.Clauzwitz.countries;
//using EuObjParser.Models.Clauzwitz.greatProjects;
//using EuObjParser.Models.Clauzwitz.religions;
//using EuObjParser.Models.Clauzwitz.shared;
//using EuObjParser.Models.Clauzwitz.ideas;
//using MoreLinq;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using YamlDotNet.Serialization;
//using EuObjParser.AAParser.Triggers;

//namespace EuObjParser.Parsing
//{
//	class FileMappingLoader
//	{
//		private readonly Eu4ModViewerConfig _config;
//		private readonly Parser _parser;
//		private readonly DataWriter _dataWriter;

//		public FileMappingLoader(
//			Eu4ModViewerConfig config,
//			Parser parser,
//			DataWriter dataWriter)
//		{
//			_config = config;
//			_parser = parser;
//			_dataWriter = dataWriter;
//		}

//		public void LoadFileMappings()
//		{
//			var baseFileMappings = _config.BaseFileMappings;
//			var baseMappingsResult = LoadBaseGameMappings();
//			var mappedModResults = _config.Mods.Select(x => LoadMappings(x, baseMappingsResult)).ToList();
//			var all = new List<FileMappingsResult>() { baseMappingsResult }.Concat(mappedModResults);
//			foreach (var item in all)
//			{
//				var id = item.Id.ToString();
//				var mod = _config.Mods.SingleOrDefault(x => x.Id == item.Id);
//				var fileMappings = mod?.FileMappings;
//				if (fileMappings == null) { fileMappings = baseFileMappings; }
//				if (item.Sections.Countries)
//				{
//					Console.WriteLine($"Combining countries for {item.ModName}");
//					var fullCountries = CombineCountryData(item.Countries, item.CountryTags, item.CountryIdeas, item.CountryHistories, item.ReligionGroups, item.CultureGroups);
//					_dataWriter.Write("countries", new { countries = fullCountries }, id);
//					CreateCountryImages(fullCountries, item.Id, true);
//				}
//				if (item.Sections.Ideas)
//				{
//					Console.WriteLine($"Combining idea groups for {item.ModName}");
//					var ideaGroups = item.IdeaGroups.Where(x => x.Trigger == null || !x.Trigger.Conditions.Any(y => y.Name == "always" && y.Value == "no"));
//					_dataWriter.Write("ideaGroups", new { ideaGroups = item.IdeaGroups }, id);
//				}
//				if (item.Sections.Policies)
//				{
//					var policies = item.Policies;
//					Console.WriteLine($"Combining policies for {item.ModName}");
//					if (fileMappings.CollapsePoliciesByAge)
//					{
//						//policies = CollapsePoliciesByAge(policies);
//					}
//					_dataWriter.Write("policies", new { policies }, id);
//				}
//				if (item.Sections.Religions)
//				{
//					Console.WriteLine($"Combining religions for {item.ModName}");
//					var fullReligionGroups = CombineReligionData(item.ReligionGroups, item.ChurchAspects);
//					_dataWriter.Write("religionGroups", new { religionGroups = fullReligionGroups }, id);
//				}
//				if (item.Sections.GreatProjects)
//				{
//					var updatedGreatProjects = CombineGreatProjectData(item.GreatProjects, item.ProvinceHistories, item.ProvinceNames);
//					_dataWriter.Write("greatProjects", new { greatProjects = updatedGreatProjects }, id);
//				}
//				_dataWriter.Write("bonuses", new { bonuses = item.Bonuses }, id);
//			}
//			_dataWriter.Write("mods", new ModList
//			{
//				Mods = _config.Mods.Concat(new Config.Mod[] { new Config.Mod { Id = 0, Name = "Base Game", FileMappings = baseFileMappings } }).Select(x =>
//				{
//					var sections = new List<ModSection>();
//					if (x.FileMappings.Sections.Countries)
//					{
//						sections.Add(new ModSection { Name = "countries", DisplayName = "Countries" });
//					}
//					if (x.FileMappings.Sections.Policies)
//					{
//						sections.Add(new ModSection { Name = "policies", DisplayName = "Policies" });
//					}
//					if (x.FileMappings.Sections.Ideas)
//					{
//						sections.Add(new ModSection { Name = "ideaGroups", DisplayName = "Idea Groups" });
//					}
//					if (x.FileMappings.Sections.Religions)
//					{
//						sections.Add(new ModSection { Name = "religionGroups", DisplayName = "Religions" });
//					}
//					if (x.FileMappings.Sections.GreatProjects)
//					{
//						sections.Add(new ModSection { Name = "greatProjects", DisplayName = "Great Projects" });
//					}
//					return new Mod { Id = x.Id, Name = x.Name, Sections = sections, Bonuses = "bonuses" };
//				}).ToList()
//			});
//		}

//		private List<Models.Output.GreatProjects.GreatProject> CombineGreatProjectData(List<GreatProject> projects, List<ProvinceHistory> histories, Dictionary<string, string> provinceNames)
//		{
//			var output = new List<Models.Output.GreatProjects.GreatProject>();
//			foreach (var project in projects)
//			{
//				var outputProj = new Models.Output.GreatProjects.GreatProject
//				{
//					BuildCost = project.BuildCost,
//					BuildTrigger = project.BuildTrigger,
//					CanBeMoved = project.CanBeMoved,
//					CanUpgradeTrigger = project.CanUpgradeTrigger,
//					CanUseModifiersTrigger = project.CanUseModifiersTrigger,
//					KeepTrigger = project.KeepTrigger,
//					Name = project.Name,
//					Start = project.Start,
//					StartingTier = project.StartingTier,
//					Tier0 = project.Tier0,
//					Tier1 = project.Tier1,
//					Tier2 = project.Tier2,
//					Tier3 = project.Tier3,
//					Type = project.Type,
//				};
//				if (provinceNames.ContainsKey("PROV" + project.Start))
//				{
//					outputProj.StartName = provinceNames["PROV" + project.Start][2..].Replace("\"", "");
//				}
//				else
//				{
//					outputProj.StartName = histories.Single(x => x.Number == project.Start).Capital;
//				}
//				output.Add(outputProj);
//			}
//			return output;
//		}

//		private void CreateCountryImages(List<Models.Output.Countries.Country> countries, long modId, bool includeBase)
//		{
//			var basePath = modId == 0
//				? _config.BaseGamePath
//				: Path.Combine(_config.ModFolderPath, modId.ToString());
//			var imageFolder = Path.Combine(basePath, "gfx", "flags");
//			var imageFileNames = Directory.Exists(imageFolder)
//				? Directory.GetFiles(imageFolder, "*")
//				: new string[0];
//			if (includeBase)
//			{
//				var baseFileNames = Directory.GetFiles(Path.Combine(_config.BaseGamePath, "gfx", "flags"));
//				imageFileNames = imageFileNames.Concat(baseFileNames).ToArray();
//			}

//			foreach (var country in countries)
//			{
//				var imageFileName = imageFileNames.FirstOrDefault(x => x.EndsWith($"{country.Tag}.tga"));
//				if (imageFileName == null)
//				{
//					continue;
//				}
//				var imageOutputFolder = Path.Combine(DataWriter.ProjectBaseFolder, "Web", "Data", modId.ToString(), "flags");
//				if (!Directory.Exists(imageOutputFolder))
//				{
//					Directory.CreateDirectory(imageOutputFolder);
//				}
//				var imageOutputPath = Path.Combine(imageOutputFolder, $"{country.Tag}.png");
//				try
//				{
//					var iconBmp = TgaBmpConv.Load(imageFileName);
//					iconBmp.Save(imageOutputPath, ImageFormat.Jpeg);
//				}
//				catch (Exception ex) { }
//			}
//		}

//		private string GetBaseGamePath(string folder) { return Path.Combine(_config.BaseGamePath, folder); }
//		private string GetModPath(string folder, long modId) { return Path.Combine(_config.ModFolderPath, modId.ToString(), folder); }

//		private BaseGameFileMappingsResult LoadBaseGameMappings()
//		{
//			var basePath = _config.BaseGamePath;
//			var mappings = _config.BaseFileMappings;
//			var sections = mappings.Sections;
//			ValidateSections(mappings);
//			Console.WriteLine("Loading Base Game files");
//			var (countries, countryFiles) = LoadFilesForMapping<Country>(GetBaseGamePath("common/countries"), mappings.Countries, flat: true);
//			var (countryTags, countryTagsFiles) = LoadFilesForMapping<KeyValuePair<string, string>>(GetBaseGamePath("common/country_tags"), mappings.CountryTags);
//			var (countryHistories, countryHistoryFiles) = LoadFilesForMapping<CountryHistory>(GetBaseGamePath("history/countries"), mappings.History.Countries, flat: true);
//			var (provinceHistories, provinceHistoryFiles) = LoadFilesForMapping<ProvinceHistory>(GetBaseGamePath("history/provinces"), mappings.History.Provinces, flat: true);
//			//var provinceNames = _fileLoader.ReadFlatYamlFile(GetBaseGamePath("localisation/" + "prov_names_l_english.yml"), "l_english");
//			var (cultures, cultureFiles) = LoadFilesForMapping<CultureGroup>(GetBaseGamePath("common/cultures"), mappings.Cultures);
//			var (ideas, ideaFiles) = LoadFilesForMapping<Models.Clauzwitz.ideas.IdeaGroup>(GetBaseGamePath("common/ideas"), mappings.Ideas, null);
//			var (greatProjects, greatProjectFiles) = LoadFilesForMapping<GreatProject>(GetBaseGamePath("common/great_projects"), mappings.GreatProjects, null);
//			var (policies, policyFiles) = LoadFilesForMapping<Models.Clauzwitz.policies.Policy>(GetBaseGamePath("common/policies"), mappings.Policies);
//			var (religions, religionFiles) = LoadFilesForMapping<ReligionGroup>(GetBaseGamePath("common/religions"), mappings.Religions);
//			var (churchAspects, churchAspectsFiles) = LoadFilesForMapping<ChurchAspect>(GetBaseGamePath("common/church_aspects"), mappings.ChurchAspects);
//			//var bonuses = religions.SelectMany(x => x.Religions.SelectMany(y => y.Country ?? new Dictionary<string, string>()).Concat(x.Religions.SelectMany(y => y.SecondaryCountry ?? new List<Bonus>())).Concat(x.Religions.SelectMany(y => y.Province ?? new List<Bonus>())))
//			//	.Concat(policies.SelectMany(x => x.Bonuses)).Concat(ideas.SelectMany(x => x.Ideas.SelectMany(y => y.Bonuses))).DistinctBy(x => x.Type).Select(x => x.ToDataType()).ToList();
//			var result = new BaseGameFileMappingsResult
//			{
//				Id = 0,
//				ModName = "Base Game",
//				Countries = countries,
//				CountriesFiles = countryFiles,
//				CountryHistories = countryHistories,
//				CountryHistoriesFiles = countryHistoryFiles,
//				CultureGroups = cultures,
//				CultureGroupsFiles = cultureFiles,
//				ChurchAspects = churchAspects,
//				ChurchAspectFiles = churchAspectsFiles,
//				CountryIdeas = ideas.Where(x => string.IsNullOrEmpty(x.Category)).ToList(),
//				IdeasFiles = ideaFiles,
//				CountryTags = countryTags.ToDictionary(x => x.Key, x => x.Value),
//				CountryTagsFiles = countryTagsFiles,
//				IdeaGroups = ideas.Where(x => !string.IsNullOrEmpty(x.Category)).ToList(),
//				Policies = policies,
//				PoliciesFiles = policyFiles,
//				ReligionGroups = religions,
//				ReligionGroupsFiles = religionFiles,
//				GreatProjects = greatProjects,
//				GreatProjectFiles = greatProjectFiles,
//				ProvinceHistories = provinceHistories,
//				ProvinceHistoriesFiles = provinceHistoryFiles,
//				//ProvinceNames = provinceNames,
//				//Bonuses = bonuses,
//				Sections = mappings.Sections
//			};

//			return result;
//		}

//		private FileMappingsResult LoadMappings(Config.Mod mod, BaseGameFileMappingsResult baseResult)
//		{
//			var mappings = mod.FileMappings;
//			var sections = mappings.Sections;
//			ValidateSections(mappings);
//			var result = new FileMappingsResult();
//			List<Models.Clauzwitz.ideas.IdeaGroup> ideas = null;
//			Console.WriteLine($"Loading {mod.Name} files");
//			if (sections.Countries)
//			{
//				(result.Countries, _) = LoadFilesForMapping(
//					GetModPath("common/countries", mod.Id),
//					mappings.Countries, baseResult.CountriesFiles, baseResult.Countries, flat: true);
//				List<KeyValuePair<string, string>> tags;
//				(tags, _) = LoadFilesForMapping<KeyValuePair<string, string>>(GetModPath("common/country_tags", mod.Id), mappings.CountryTags, baseResult.CountryTagsFiles, baseResult.CountryTags.ToList());
//				result.CountryTags = tags.ToDictionary(x => x.Key, x => x.Value);
//				(result.CountryHistories, _) = LoadFilesForMapping(GetModPath("history/countries", mod.Id), mappings.History.Countries, baseResult.CountryHistoriesFiles, baseResult.CountryHistories, flat: true);
//				(result.CultureGroups, _) = LoadFilesForMapping(GetModPath("common/cultures", mod.Id), mappings.Cultures, baseResult.CultureGroupsFiles, baseResult.CultureGroups);
//				(ideas, _) = LoadFilesForMapping(GetModPath("common/ideas", mod.Id), mappings.Ideas, baseResult.IdeasFiles, baseResult.IdeaGroups.Concat(baseResult.CountryIdeas).ToList());
//				result.CountryIdeas = ideas.Where(x => string.IsNullOrEmpty(x.Category)).ToList();
//			}
//			if (sections.Policies)
//			{
//				(result.Policies, _) = LoadFilesForMapping(GetModPath("common/policies", mod.Id), mappings.Policies, baseResult.PoliciesFiles, baseResult.Policies);
//				if (!sections.Ideas)
//				{
//					if (ideas == null)
//					{
//						(ideas, _) = LoadFilesForMapping(GetModPath("common/ideas", mod.Id), mappings.Ideas, baseResult.IdeasFiles, baseResult.IdeaGroups.Concat(baseResult.CountryIdeas).ToList());
//					}
//					result.IdeaGroups = ideas.Where(x => !string.IsNullOrEmpty(x.Category)).ToList();
//				}
//			}
//			if (sections.Ideas)
//			{
//				if (ideas == null)
//				{
//					(ideas, _) = LoadFilesForMapping(GetModPath("common/ideas", mod.Id), mappings.Ideas, baseResult.IdeasFiles, baseResult.IdeaGroups.Concat(baseResult.CountryIdeas).ToList());
//				}
//				result.IdeaGroups = ideas.Where(x => !string.IsNullOrEmpty(x.Category)).ToList();
//			}

//			if (sections.Religions)
//			{
//				(result.ReligionGroups, _) = LoadFilesForMapping(GetModPath("common/religions", mod.Id), mappings.Religions, baseResult.ReligionGroupsFiles, baseResult.ReligionGroups);
//				(result.ChurchAspects, _) = LoadFilesForMapping(GetModPath("common/church_aspects", mod.Id), mappings.ChurchAspects, baseResult.ChurchAspectFiles, baseResult.ChurchAspects);
//			}
//			if (sections.GreatProjects)
//			{
//				(result.GreatProjects, _) = LoadFilesForMapping(GetModPath("common/great_projects", mod.Id), mappings.GreatProjects, baseResult.GreatProjectFiles, baseResult.GreatProjects);
//				(result.ProvinceHistories, _) = LoadFilesForMapping(GetModPath("history/provinces", mod.Id), mappings.History.Provinces, baseResult.ProvinceHistoriesFiles, baseResult.ProvinceHistories, flat: true);
//				//result.ProvinceNames = _fileLoader.ReadFlatYamlFile(GetModPath("localisation/" + "prov_names_l_english.yml", mod.Id), "l_english");
//			}
//			//var religionBonuses = result.ReligionGroups == null ? new List<Bonus>() : result.ReligionGroups.SelectMany(x => x.Religions.SelectMany(y => y.Country ?? new List<Bonus>()).Concat(x.Religions.SelectMany(y => y.SecondaryCountry ?? new List<Bonus>())).Concat(x.Religions.SelectMany(y => y.Province ?? new List<Bonus>())));
//			//var policyBonuses = result.Policies == null
//			//	? new List<Bonus>()
//			//	: result.Policies.SelectMany(x => x.Bonuses);
//			//var bonuses = religionBonuses.Concat(policyBonuses);
//			//if (ideas != null)
//			//{
//			//	bonuses = bonuses.Concat(ideas.SelectMany(x => x.Ideas?.SelectMany(y => y.Bonuses ?? new List<Bonus>()) ?? new List<Bonus>()));
//			//}
//			//result.Bonuses = bonuses.DistinctBy(x => x.Type).Select(x => x.ToDataType()).ToList();
//			result.Sections = mappings.Sections;
//			result.ModName = mod.Name;
//			result.Id = mod.Id;
//			return result;
//		}

//		private void ValidateSections(FileMappings mappings)
//		{
//			var sections = mappings.Sections;
//			if (sections.Countries)
//			{
//				if (mappings.Countries == null
//					|| mappings.CountryTags == null
//					|| mappings.Cultures == null
//					|| mappings.History?.Countries == null
//					|| mappings.Ideas == null)
//				{
//					throw new Exception("Countries section is missing required config");
//				}
//			}
//			if (sections.Policies)
//			{
//				if (mappings.Policies == null
//					|| mappings.Ideas == null)
//				{
//					throw new Exception("Policies section is missing required config");
//				}
//			}
//			if (sections.Ideas)
//			{
//				if (mappings.Ideas == null)
//				{
//					throw new Exception("Idea groups section is missing required config");
//				}
//			}
//			if (sections.Religions)
//			{
//				if (mappings.Religions == null || mappings.ChurchAspects == null)
//				{
//					throw new Exception("Religions section is missing required config");
//				}
//			}
//		}

//		private (List<T>, List<string>) LoadFilesForMapping<T>(
//			string filePath,
//			FileMapping mapping,
//			List<string> baseGameNames = null,
//			List<T> baseGameItems = null,
//			bool flat = false)
//		{
//			List<string> names = mapping.Files;
//			if (mapping.Base)
//			{
//				return (baseGameItems, baseGameNames);
//			}
//			if (mapping.BaseNames)
//			{
//				names = names == null
//					? baseGameNames
//					: names.Concat(baseGameNames).ToList();
//			}
//			IEnumerable<string> files;
//			if ((mapping.Ignore != null && mapping.Ignore.Any()))
//			{
//				files = Directory.GetFiles(filePath).Where(x => !mapping.Ignore.Any(y => x.EndsWith(y)));
//			} else if(names != null && names.Any())
//			{
//				files = Directory.GetFiles(filePath).Where(x => names.Any(y => x.EndsWith(y)));
//			}
//			else
//			{
//				files = Directory.GetFiles(filePath);
//			}
			
//			var fileNames = files.Select(x => x.Split("\\").Last()).ToList();
//			var output = new List<T>();
//			foreach(var fileName in fileNames)
//			{
//				if (flat)
//				{
//					output.Add(_parser.Parse<T>(fileName, filePath));
//				}
//				else
//				{
//					output.AddRange(_parser.Parse<IReadOnlyCollection<T>>(fileName, filePath));
//				}
//			}
//			if (mapping.Collapse)
//			{
//				output = output.Concat(baseGameItems).Distinct().ToList();
//			}
//			return (output, fileNames);

//		}

//		private List<Models.Output.Religions.ReligionGroup> CombineReligionData(
//			List<ReligionGroup> religionGroups,
//			List<ChurchAspect> blessings)
//		{
//			return religionGroups.Select(group => new Models.Output.Religions.ReligionGroup
//			{
//				CanFormPersonalUnions = group.CanFormPersonalUnions,
//				DefenderOfFaith = group.DefenderOfFaith,
//				Name = group.Name,
//				Religions = group.Religions.Select(religion => new Models.Output.Religions.Religion
//				{
//					Name = religion.Name,
//					Color = religion.Color,
//					Country = religion.Country,
//					SecondaryCountry = religion.SecondaryCountry,
//					Province = religion.Province,
//					Blessings = religion.Blessings
//						?.Select(x => blessings.SingleOrDefault(y => y.Name == x))
//						?.ToList(),
//					Aspects = religion.Aspects
//						?.Select(x => blessings.SingleOrDefault(y => y.Name == x))
//						?.ToList()
//				}).ToList()
//			}).ToList();
//		}
//		private string CountryNameMap(string name)
//		{
//			return name.ToLower() switch
//			{
//				"ukraine" => "Ukraine",
//				"shu" => "Shun",
//				"qic" => "QIC",
//				"min" => "Min",
//				"chw" => "Bachwezi",
//				"chn" => "China",
//				"afr" => "Toto",
//				"peu" => "Peru",
//				"lou" => "Louisiana",
//				"cub" => "Cuba",
//				"lap" => "La Plata",
//				"vnz" => "Venezuela",
//				"gzw" => "Great Zimbabwe",
//				_ => Regex.Replace(name, "([a-z]{1})([A-Z]{1})", "$1 $2"),
//			};
//		}

//		private List<Models.Output.Countries.Country> CombineCountryData(
//			List<Country> countries,
//			IReadOnlyDictionary<string, string> countryTags,
//			List<IdeaGroup> countryIdeas,
//			List<CountryHistory> countryHistories,
//			List<ReligionGroup> religionGroups,
//			List<CultureGroup> cultureGroups)
//		{
//			var fullCountries = new List<Models.Output.Countries.Country>();
//			var countryHistoryDict = countryHistories.ToDictionary(x => x.FileName.Split(new string[] {" - ", " -", "- ", "-" }, StringSplitOptions.RemoveEmptyEntries)[0], x => x);
//			var cultureGroupsDict = cultureGroups.ToDictionary(x => x.Name, x => x);
//			//var culturesDict = cultureGroups.SelectMany(x => x.Cultures).ToDictionary(x => x.Name, x => x);
//			countryTags = countryTags.Where(x => x.Key != "REB" && x.Key != "PIR" && x.Key != "NAT" && x.Key != "SYN").ToDictionary(x => x.Key, x => x.Value);
//			foreach (var tag in countryTags)
//			{
//				if (!countryHistoryDict.ContainsKey(tag.Key))
//				{
//					continue;
//				}
//				var history = countryHistoryDict[tag.Key];
//				var primaryCultureGroupName = cultureGroups.Single(x => x.Cultures.Any(y => y.Key == history.PrimaryCulture)).Name;
//				var religionGroupName = religionGroups.Single(x => x.Religions.Any(y => y.Name == history.Religion)).Name;
//				var query = new CountryIdeaQuery
//				{
//					Tag = tag.Key,
//					CultureGroup = primaryCultureGroupName,
//					PrimaryCulture = history.PrimaryCulture,
//					Reforms = history.AddGovernmentReform,
//					TechnologyGroup = history.TechnologyGroup,
//					Religion = history.Religion,
//					ReligionGroup = religionGroupName
//				};
//				var matchingIdea = countryIdeas.SingleOrDefault(x => CountryIdeaTriggerResolver.Matches(x.Trigger, query));
//				if(matchingIdea == null)
//				{
//					continue;
//				}
//				var name = tag.Value.Replace("\"", "").Split("/").Last();
//				var country = countries.SingleOrDefault(x => x.Name == name);
//				fullCountries.Add(new Models.Output.Countries.Country
//				{
//					Tag = tag.Key,
//					Name = CountryNameMap(name),
//					Ideas = matchingIdea.Ideas,
//					Colors = country == null
//						? new List<Color>()
//						: new List<Color> { country.Color }
//				});
//			}
//			return fullCountries;
//		}

//		private readonly Dictionary<int, string> _ages = new Dictionary<int, string>
//		{
//			[0] = "age_of_discovery",
//			[1] = "age_of_reformation",
//			[2] = "age_of_absolutism",
//			[3] = "age_of_revolutions",
//		};

//		//private List<Policy> CollapsePoliciesByAge(List<Policy> policies)
//		//{
//		//	var policyAggregate = policies.Aggregate(new
//		//	{
//		//		dict = new Dictionary<string, Dictionary<string, List<Bonus>>>(),
//		//		output = new List<Policy>()
//		//	}, (prev, cur) =>
//		//	{
//		//		if (!string.IsNullOrEmpty(cur.Allow.CurrentAge))
//		//		{
//		//			if (!prev.dict.ContainsKey(cur.DisplayName))
//		//			{
//		//				prev.dict[cur.DisplayName] = new Dictionary<string, List<Bonus>>();
//		//			}
//		//			prev.dict[cur.DisplayName][cur.Allow.CurrentAge] = cur.Bonuses;
//		//			if (prev.dict[cur.DisplayName].Count == 4)
//		//			{
//		//				var bonuses1 = prev.dict[cur.DisplayName][_ages[0]];
//		//				var bonuses2 = prev.dict[cur.DisplayName][_ages[1]];
//		//				var bonuses3 = prev.dict[cur.DisplayName][_ages[2]];
//		//				var bonuses4 = prev.dict[cur.DisplayName][_ages[3]];
//		//				prev.output.Add(new Policy
//		//				{
//		//					Name = cur.Name,
//		//					Allow = cur.Allow,
//		//					MonarchPower = cur.MonarchPower,
//		//					Bonuses = bonuses1.Select(x => new Bonus
//		//					{
//		//						Type = x.Type,
//		//						Value = x.Value + "/" + bonuses2.Single(y => y.Type == x.Type).Value + "/" + bonuses3.Single(y => y.Type == x.Type).Value + "/" + bonuses4.Single(y => y.Type == x.Type).Value
//		//					}).ToList()
//		//				});
//		//				return prev;
//		//			}
//		//			else { return prev; }
//		//		}
//		//		prev.output.Add(cur);
//		//		return prev;
//		//	});
//		//	return policyAggregate.output;
//		//}
//	}
//}
