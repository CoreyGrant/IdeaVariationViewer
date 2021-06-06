using EuObjParser.AAParser.Triggers;
using EuObjParser.Config;
using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Models.Json;
using EuObjParser.Parsing;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Pfim;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Runtime.InteropServices;
using Color = EuObjParser.Models.Clauzwitz.shared.Color;

namespace EuObjParser.AAParser
{
	class ClauzwitzJsonProcessor
	{
		private readonly DataWriter _dataWriter;
		private readonly Eu4ModViewerConfig _config;
		private readonly ClauzwitzToJsonFolderConverter _converter;
		private readonly ClauzwitzJsonLoader _jsonLoader;

		public ClauzwitzJsonProcessor(
			DataWriter dataWriter,
			Eu4ModViewerConfig config,
			ClauzwitzToJsonFolderConverter clauzwitzToJsonFolderConverter,
			ClauzwitzJsonLoader jsonLoader)
		{
			_dataWriter = dataWriter;
			_config = config;
			_converter = clauzwitzToJsonFolderConverter;
			_jsonLoader = jsonLoader;
		}

		public void Process(bool rebuildGameJson)
		{
			var modFolder = _config.ModFolderPath;

			// Convert all mods and basegame
			ProcessMod(_config.BaseGamePath, 0, _config.BaseFileMappings, rebuildGameJson);
			foreach (var mod in _config.Mods)
			{
				var path = Path.Combine(modFolder, mod.Id.ToString());
				ProcessMod(path, mod.Id, mod.FileMappings, rebuildGameJson);
			}
		}

		private List<T> MapFiles<T>(Func<string, string[], List<T>> loader, FileMapping mapping, string modPath, Func<T, string> uniqueBy)
		{
			return mapping.All
				? loader(modPath, mapping.Except?.ToArray())
				: mapping.Base
					? loader(baseTempFolder, mapping.Except?.ToArray())
					: mapping.Collapse
						? loader(modPath, mapping.Except?.ToArray()).Concat(loader(baseTempFolder, mapping.Except?.ToArray())).DistinctBy(uniqueBy).ToList()
						: throw new Exception();

		}
		private readonly string baseTempFolder = Path.Combine(DataWriter.ProjectBaseFolder, "Temp", "0");
		private void ProcessMod(string gameDataPath, long modId, FileMappings fileMappings, bool rebuildGameJson)
		{
			if (rebuildGameJson)
			{
				_converter.Convert(gameDataPath, modId);
			}
			var tempFolderForMod = Path.Combine(DataWriter.ProjectBaseFolder, "Temp", modId.ToString());

			var localizations = _jsonLoader.LoadLocalizations(tempFolderForMod).Concat(
				_jsonLoader.LoadLocalizations(Path.Combine(DataWriter.ProjectBaseFolder, "Temp", "0"))).DistinctBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

			var countries = MapFiles(_jsonLoader.LoadCountries, fileMappings.Countries, tempFolderForMod, x => x.Filename);

			var countryHistories = MapFiles(_jsonLoader.LoadHistoryCountries, fileMappings.History.Countries, tempFolderForMod, x => x.Filename);
			var countryTags = MapFiles(_jsonLoader.LoadCountryTags, fileMappings.CountryTags, tempFolderForMod, x => x.Key);
			var ideas = MapFiles(_jsonLoader.LoadIdeas, fileMappings.Ideas, tempFolderForMod, x => x.Name);
			var policies = MapFiles(_jsonLoader.LoadPolicies, fileMappings.Policies, tempFolderForMod, x => x.Name);
			var religions = MapFiles(_jsonLoader.LoadReligions, fileMappings.Religions, tempFolderForMod, x => x.Name);
			var cultures = MapFiles(_jsonLoader.LoadCultures, fileMappings.Cultures, tempFolderForMod, x => x.Name);

			var countryIdeas = ideas.Where(x => string.IsNullOrEmpty(x.Category)).ToList();
			var ideaGroups = ideas.Where(x => !string.IsNullOrEmpty(x.Category)).ToList();

			// filter the country histories based on the country tags
			countryHistories = countryHistories.Where(x => countryTags.Any(ct => ct.Value.Split('/')[1].Trim() == x.Filename.Split('-')[1].Trim())).ToList();

			var religionsOutput = CombineReligionData(religions);
			var countriesOutput = CombineCountryData(countries, countryTags.ToDictionary(x => x.Key, x => x.Value), countryIdeas, countryHistories, religions, cultures);
			var ideaGroupsOutput = CombineIdeaGroups(ideaGroups, localizations);
			var policiesOutput = policies.Select(x => new Models.Output.Policies.Policy
			{
				Name = x.Name,
				Allow = x.Allow,
				Potential = x.Potential,
				Bonuses = x.Bonuses,
				MonarchPower = x.MonarchPower
			}).ToList();

			var religionBonuses = religionsOutput.SelectMany(x => x.Religions.SelectMany(y =>
				(y.Province ?? new Dictionary<string, string>()).Concat(y.SecondaryCountry ?? new Dictionary<string, string>()).Concat(y.Province ?? new Dictionary<string, string>()))
			).ToList();
			var ideaBonuses = ideas.SelectMany(x => x.Ideas.SelectMany(y => y.Bonuses)).ToList();
			var policyBonuses = policies.SelectMany(x => x.Bonuses).ToList();
			var bonuses = religionBonuses
				.Concat(ideaBonuses)
				.Concat(policyBonuses)
				.Select(x => x.Key)
				.Distinct().ToList();

			_dataWriter.Write("bonuses", new { bonuses = bonuses }, "Web\\Data\\" + modId.ToString());
			_dataWriter.Write("religionGroups", new { religionGroups = religionsOutput }, "Web\\Data\\" + modId.ToString());
			_dataWriter.Write("countries", new { countries = countriesOutput }, "Web\\Data\\" + modId.ToString());
			_dataWriter.Write("ideaGroups", new { ideaGroups = ideaGroupsOutput }, "Web\\Data\\" + modId.ToString());
			_dataWriter.Write("policies", new { policies = policiesOutput }, "Web\\Data\\" + modId.ToString());

			CreateCountryImages(countriesOutput, modId, true);
			CreateBonusImages(bonuses, modId);

			_dataWriter.Write("mods", new Parsing.ModList
			{
				Mods = _config.Mods.Concat(new Config.Mod[] { new Config.Mod { Id = 0, Name = "Base Game", FileMappings = _config.BaseFileMappings } }).Select(x =>
				{
					var sections = new List<ModSection>();
					if (x.FileMappings.Sections.Countries)
					{
						sections.Add(new ModSection { Name = "countries", DisplayName = "Countries" });
					}
					if (x.FileMappings.Sections.Policies)
					{
						sections.Add(new ModSection { Name = "policies", DisplayName = "Policies" });
					}
					if (x.FileMappings.Sections.Ideas)
					{
						sections.Add(new ModSection { Name = "ideaGroups", DisplayName = "Idea Groups" });
					}
					if (x.FileMappings.Sections.Religions)
					{
						sections.Add(new ModSection { Name = "religionGroups", DisplayName = "Religions" });
					}
					//if (x.FileMappings.Sections.GreatProjects)
					//{
					//	sections.Add(new ModSection { Name = "greatProjects", DisplayName = "Great Projects" });
					//}
					return new Parsing.Mod { Id = x.Id, Name = x.Name, Sections = sections, Bonuses = "bonuses" };
				}).ToList()
			}, "Web\\Data\\");
		}
		private const char ParadoxReplacement = '§';
		private List<Models.Output.Ideas.IdeaGroup> CombineIdeaGroups(List<IdeaGroup> ideas, IReadOnlyDictionary<string, string> localizations)
		{
			var outputIdeas = new List<Models.Output.Ideas.IdeaGroup>();
			foreach(var idea in ideas)
			{
				outputIdeas.Add(new Models.Output.Ideas.IdeaGroup
				{
					Name = idea.Name,
					Trigger = idea.Trigger,
					Category = idea.Category,
					Ideas = idea.Ideas.Select(i => new Models.Output.Ideas.Idea
					{
						Name = i.Name,
						Bonuses = i.Bonuses,
						LocalizedDesc = GetLocalization(i.Name + "_desc", localizations),
						LocalizedName = GetLocalization(i.Name, localizations)?.Split(ParadoxReplacement)?[0],
					}).ToList(),
					LocalizedDesc = GetLocalization(idea.Name + "_desc", localizations),
					LocalizedName = GetLocalization(idea.Name, localizations)?.Split(ParadoxReplacement)?[0],
				});
			}
			return outputIdeas;
		}

		private string GetLocalization(string key, IReadOnlyDictionary<string, string> loc)
		{
			var lowerKey = key.ToLower();
			return loc.ContainsKey(lowerKey)
				? loc[lowerKey]
				: null;
		}

		private List<Models.Output.Religions.ReligionGroup> CombineReligionData(
			List<ReligionGroup> religionGroups/*,
			List<ChurchAspect> blessings*/)
		{
			return religionGroups.Select(group => new Models.Output.Religions.ReligionGroup
			{
				CanFormPersonalUnions = group.CanFormPersonalUnions == "yes",
				DefenderOfFaith = group.DefenderOfFaith == "yes",
				Name = group.Name,
				Religions = group.Religions.Select(religion => new Models.Output.Religions.Religion
				{
					Name = religion.Name,
					Color = new Color { Red = (int)Math.Round(decimal.Parse(religion.Color[0])), Green = (int)Math.Round(decimal.Parse(religion.Color[1])), Blue = (int)Math.Round(decimal.Parse(religion.Color[2])) },
					Country = religion.Country,
					SecondaryCountry = religion.CountryAsSecondary,
					Province = religion.Province,
					//Blessings = religion.Blessings
					//	?.Select(x => blessings.SingleOrDefault(y => y.Name == x))
					//	?.ToList(),
					//Aspects = religion.Aspects
					//	?.Select(x => blessings.SingleOrDefault(y => y.Name == x))
					//	?.ToList()
				}).ToList()
			}).ToList();
		}
		private string CountryNameMap(string name)
		{
			return name.ToLower() switch
			{
				"ukraine" => "Ukraine",
				"shu" => "Shun",
				"qic" => "QIC",
				"min" => "Min",
				"chw" => "Bachwezi",
				"chn" => "China",
				"afr" => "Toto",
				"peu" => "Peru",
				"lou" => "Louisiana",
				"cub" => "Cuba",
				"lap" => "La Plata",
				"vnz" => "Venezuela",
				"gzw" => "Great Zimbabwe",
				_ => Regex.Replace(name, "([a-z]{1})([A-Z]{1})", "$1 $2"),
			};
		}

		private List<Models.Output.Countries.Country> CombineCountryData(
			List<Country> countries,
			IReadOnlyDictionary<string, string> countryTags,
			List<IdeaGroup> countryIdeas,
			List<CountryHistory> countryHistories,
			List<ReligionGroup> religionGroups,
			List<CultureGroup> cultureGroups)
		{
			var fullCountries = new List<Models.Output.Countries.Country>();
			var countryHistoryDict = countryHistories.ToDictionary(x => x.Filename.Split(new string[] { " - ", " -", "- ", "-" }, StringSplitOptions.RemoveEmptyEntries)[0], x => x);
			var cultureGroupsDict = cultureGroups.ToDictionary(x => x.Name, x => x);
			//var culturesDict = cultureGroups.SelectMany(x => x.Cultures).ToDictionary(x => x.Name, x => x);
			countryTags = countryTags.Where(x => x.Key != "REB" && x.Key != "PIR" && x.Key != "NAT" && x.Key != "SYN" && x.Key != "PAP").ToDictionary(x => x.Key, x => x.Value);
			foreach (var tag in countryTags)
			{
				if (!countryHistoryDict.ContainsKey(tag.Key))
				{
					continue;
				}
				var history = countryHistoryDict[tag.Key];
				var primaryCultureGroupName = cultureGroups.Single(x => x.Cultures.Any(y => y.Name == history.PrimaryCulture)).Name;
				var religionGroupName = religionGroups.Single(x => x.Religions.Any(y => y.Name == history.Religion)).Name;
				var query = new CountryIdeaQuery
				{
					Tag = tag.Key,
					CultureGroup = primaryCultureGroupName,
					PrimaryCulture = history.PrimaryCulture,
					Reforms = history.AddGovernmentReform,
					TechnologyGroup = history.TechnologyGroup,
					Religion = history.Religion,
					ReligionGroup = religionGroupName
				};
				var matchingIdeas = countryIdeas.Where(x => CountryIdeaTriggerResolver.Matches(x.Trigger, query)).ToList();
				var matchingIdea = MostSpecificIdeaGroup(matchingIdeas, query);
				if(matchingIdea == null) { continue; }
				var name = tag.Value.Replace("\"", "").Split("/").Last().Replace(".txt", "");
				var country = countries.SingleOrDefault(x => x.Filename.Replace(".txt", "") == name);
				var countryColor = country?.Color?.ToList();
				fullCountries.Add(new Models.Output.Countries.Country
				{
					Tag = tag.Key,
					Name = CountryNameMap(name),
					Ideas = matchingIdea.Ideas,
					Colors = country == null
						? new List<Models.Clauzwitz.shared.Color>()
						: new List<Models.Clauzwitz.shared.Color> { new Models.Clauzwitz.shared.Color { Red = int.Parse(countryColor[0]), Green = int.Parse(countryColor[1]), Blue = int.Parse(countryColor[2]) } }
				});
			}
			return fullCountries;
		}

		private IdeaGroup MostSpecificIdeaGroup(List<IdeaGroup> ideaGroups, CountryIdeaQuery query)
		{
			if(ideaGroups.Count == 0)
			{
				return null;
			}
			if(ideaGroups.Count == 1)
			{
				return ideaGroups.Single();
			}

			// Order of precidence (assuming)
			// Tag -> (Culture -> Culture Group)/(Religion -> Religion Group)/(Technology Group)/(Government Reform)
			
			// Alternatively, most important idea group might always be first, apart from default
			if(ideaGroups.Where(x => x.Name != "default_ideas").Count() >= 2)
			{
				return ideaGroups.First();
			}
			var cultureQuery = new CountryIdeaQuery
			{
				PrimaryCulture = query.PrimaryCulture
			};
			var cultureIdea = ideaGroups.Where(x => x.Trigger != null).SingleOrDefault(ig => CountryIdeaTriggerResolver.Matches(ig.Trigger, cultureQuery));
			if(cultureIdea != null)
			{
				return cultureIdea;
			}

			var cultureGroupQuery = new CountryIdeaQuery
			{
				CultureGroup = query.CultureGroup
			};
			var cultureGroupIdea = ideaGroups.Where(x => x.Trigger != null).SingleOrDefault(ig => CountryIdeaTriggerResolver.Matches(ig.Trigger, cultureGroupQuery));
			if (cultureGroupIdea != null)
			{
				return cultureGroupIdea;
			}

			var notDefault = ideaGroups.SingleOrDefault(ig => ig.Name != "default_ideas");
			if(notDefault != null)
			{
				return notDefault;
			}
			return ideaGroups.SingleOrDefault(ig => ig.Name == "default_ideas");
		}

		private readonly Dictionary<int, string> _ages = new Dictionary<int, string>
		{
			[0] = "age_of_discovery",
			[1] = "age_of_reformation",
			[2] = "age_of_absolutism",
			[3] = "age_of_revolutions",
		};
		private readonly Pfim.PfimConfig pfimConfig = new PfimConfig(applyColorMap: false);
		private void CreateBonusImages(List<string> bonuses, long modId)
		{
			var modPath = modId != 0 
				? Path.Combine(_config.ModFolderPath, modId.ToString())
				: _config.BaseGamePath;
			var imageFolder = Path.Combine(modPath, "gfx", "interface", "ideas_EU4");
			var baseImageFolder = Path.Combine(_config.BaseGamePath, "gfx", "interface", "ideas_EU4");
			var bonusesFolder = Path.Combine(DataWriter.ProjectBaseFolder, "Web", "Icons", "Bonuses");
			foreach(var bonus in bonuses)
			{
				var filename = bonus + ".png";
				var fullFilename = Path.Combine(DataWriter.ProjectBaseFolder, "Temp", "Bonuses", filename);
				if (File.Exists(fullFilename))
				{
					continue;
				}
				// Does the output file exist?
				if(File.Exists(Path.Combine(bonusesFolder, filename)))
				{
					continue;
				}
				var imageFilePath = Path.Combine(imageFolder, bonus + ".dds");
				if (File.Exists(imageFilePath))
				{
					try
					{
						File.WriteAllBytes(Path.Combine(DataWriter.ProjectBaseFolder, "Temp", "Bonuses", bonus + ".dds"), File.ReadAllBytes(imageFilePath));
						//using(var image = Pfim.Pfim.FromFile(imageFilePath, pfimConfig))
						//{
						//	var bitmap = new Bitmap(image.Width, image.Height, image.Stride, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0));
						//	bitmap.Save(fullFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
						//}
					}
					catch (Exception ex) { }
				}
				var baseImageFilePath = Path.Combine(baseImageFolder, bonus + ".dds");
				if (File.Exists(baseImageFilePath))
				{
					try
					{
						File.WriteAllBytes(Path.Combine(DataWriter.ProjectBaseFolder, "Temp", "Bonuses", bonus + ".dds"), File.ReadAllBytes(baseImageFilePath));
						//using(var image = Pfim.Pfim.FromFile(imageFilePath, pfimConfig))
						//{
						//	var bitmap = new Bitmap(image.Width, image.Height, image.Stride, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0));
						//	bitmap.Save(fullFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
						//}
					}
					catch (Exception ex) { }
				}

			}
		}
		
		private void CreateCountryImages(List<Models.Output.Countries.Country> countries, long modId, bool includeBase)
		{
			var basePath = modId == 0
				? _config.BaseGamePath
				: Path.Combine(_config.ModFolderPath, modId.ToString());
			var imageFolder = Path.Combine(basePath, "gfx", "flags");
			var imageFileNames = Directory.Exists(imageFolder)
				? Directory.GetFiles(imageFolder, "*")
				: new string[0];
			if (includeBase)
			{
				var baseFileNames = Directory.GetFiles(Path.Combine(_config.BaseGamePath, "gfx", "flags"));
				imageFileNames = imageFileNames.Concat(baseFileNames).ToArray();
			}

			foreach (var country in countries)
			{
				var imageFileName = imageFileNames.FirstOrDefault(x => x.EndsWith($"{country.Tag}.tga"));
				if (imageFileName == null)
				{
					continue;
				}
				var imageOutputFolder = Path.Combine(DataWriter.ProjectBaseFolder, "Web", "Data", modId.ToString(), "flags");
				if (!Directory.Exists(imageOutputFolder))
				{
					Directory.CreateDirectory(imageOutputFolder);
				}
				var imageOutputPath = Path.Combine(imageOutputFolder, $"{country.Tag}.png");
				try
				{
					var iconBmp = TgaBmpConv.Load(imageFileName);
					iconBmp.Save(imageOutputPath, System.Drawing.Imaging.ImageFormat.Jpeg);
				}
				catch (Exception ex) { }
			}
		}

		//private List<Policy> CollapsePoliciesByAge(List<Policy> policies)
		//{
		//	var policyAggregate = policies.Aggregate(new
		//	{
		//		dict = new Dictionary<string, Dictionary<string, List<Bonus>>>(),
		//		output = new List<Policy>()
		//	}, (prev, cur) =>
		//	{
		//		if (!string.IsNullOrEmpty(cur.Allow.CurrentAge))
		//		{
		//			if (!prev.dict.ContainsKey(cur.DisplayName))
		//			{
		//				prev.dict[cur.DisplayName] = new Dictionary<string, List<Bonus>>();
		//			}
		//			prev.dict[cur.DisplayName][cur.Allow.CurrentAge] = cur.Bonuses;
		//			if (prev.dict[cur.DisplayName].Count == 4)
		//			{
		//				var bonuses1 = prev.dict[cur.DisplayName][_ages[0]];
		//				var bonuses2 = prev.dict[cur.DisplayName][_ages[1]];
		//				var bonuses3 = prev.dict[cur.DisplayName][_ages[2]];
		//				var bonuses4 = prev.dict[cur.DisplayName][_ages[3]];
		//				prev.output.Add(new Policy
		//				{
		//					Name = cur.Name,
		//					Allow = cur.Allow,
		//					MonarchPower = cur.MonarchPower,
		//					Bonuses = bonuses1.Select(x => new Bonus
		//					{
		//						Type = x.Type,
		//						Value = x.Value + "/" + bonuses2.Single(y => y.Type == x.Type).Value + "/" + bonuses3.Single(y => y.Type == x.Type).Value + "/" + bonuses4.Single(y => y.Type == x.Type).Value
		//					}).ToList()
		//				});
		//				return prev;
		//			}
		//			else { return prev; }
		//		}
		//		prev.output.Add(cur);
		//		return prev;
		//	});
		//	return policyAggregate.output;
		//}
	}
}
