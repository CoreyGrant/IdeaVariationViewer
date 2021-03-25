using EuObjParser.Enums;
using EuObjParser.Models;
using EuObjParser.Models.Base;
using EuObjParser.Models.Parsing;
using EuObjParser.Parsing;
using EuObjParser.Parsing.Converting;
using MoreLinq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EuObjParser
{
	class Program
	{
		static string AnbennarFileLocation = @"C:\Program Files (x86)\Steam\steamapps\workshop\content\236850\2370830316\common\";
		static string AnbennarDataLocation = @"C:\Users\corey\Desktop\CoreyGrant.github.io\Data\Anbennar\";

		//private static void LoadAnbennar()
		//{
		//	var converter = new EuObjToJsonConverter();
		//	var parser = new Parser();
		//	var countryFileNames = Directory.GetFiles(AnbennarFileLocation + "countries");
		//	var countryFiles = countryFileNames.Select(x => new
		//	{
		//		Text = File.ReadAllText(x),
		//		Name = new Regex("[\\w. ]+$").Match(x).Value.Replace(".txt", "")
		//	}).Where(x => x.Name != "Y89");
		//	var allCountries = countryFiles.Aggregate("", (prev, cur) =>
		//	 {
		//		 var lines = cur.Text.Split('\n');
		//		 var colorLine = lines.First(x => x.Trim().StartsWith("color"));
		//		 return prev + $"{cur.Name.Split(".txt")[0]} = {{\n{colorLine}\n}}\n";
		//	 });
		//	var countryTags = File.ReadAllText(AnbennarFileLocation + "country_tags\\anb_countries.txt");
		//	var countryColors = File.ReadAllText(AnbennarFileLocation + "country_colors\\anb_country_colors.txt");

		//	var countryIdeas = File.ReadAllText(AnbennarFileLocation + "ideas\\anb_country_ideas.txt");
		//	var genericCountryIdeas = File.ReadAllText(AnbennarFileLocation + "ideas\\anb_group_ideas.txt");
		//	//var allCountiesJson = "{" + converter.Convert(allCountries) + "}";
		//	var countryTagsJson = "{" + converter.ConvertToJsonString(countryTags, false) + "}";
		//	var countryIdeasJson = "{" + converter.ConvertToJsonString(countryIdeas, false) + "}";
		//	var genericCountryIdeasJson = "{" + converter.ConvertToJsonString(genericCountryIdeas, false) + "}";
		//	var countryColorsJson = "{" + converter.ConvertToJsonString(countryColors, false) + "}";
		//	var allCountriesJson = "{" + converter.ConvertToJsonString(allCountries, false) + "}";
		//	//File.WriteAllText(AnbennarDataLocation + "AllCountries.json", allCountiesJson);
		//	File.WriteAllText(AnbennarDataLocation + "CountryTags.json", countryTagsJson);
		//	File.WriteAllText(AnbennarDataLocation + "CountryIdeas.json", countryIdeasJson);
		//	File.WriteAllText(AnbennarDataLocation + "GenericCountryIdeas.json", countryIdeasJson);
		//	File.WriteAllText(AnbennarDataLocation + "CountryColors.json", countryColorsJson);
		//	File.WriteAllText(AnbennarDataLocation + "AllCountries.json", allCountriesJson);
		//	// Parse into objects

		//	var countries = parser.ParseCountries(countryTagsJson, countryIdeasJson, countryColorsJson, allCountriesJson);
		//	var genericCountries = parser.ParseGenericCountries(genericCountryIdeasJson);
		//	var concatedCountries = countries.Concat(genericCountries).ToList();
		//	concatedCountries.Sort((x, y) => string.Compare(x.Name, y.Name));
		//	var bonuses = countries.Concat(genericCountries).SelectMany(x => x.Ideas.SelectMany(x => x.Bonuses))
		//		.DistinctBy(x => x.Type).Select(x => x.Copy()).ToList();
		//	foreach (var bonus in bonuses)
		//	{
		//		bonus.Value = null;
		//	}
		//	// Output objects in one file
		//	DefaultContractResolver contractResolver = new DefaultContractResolver
		//	{
		//		NamingStrategy = new CamelCaseNamingStrategy()
		//	};
		//	File.WriteAllText(@"C:\Users\corey\Desktop\CoreyGrant.github.io\Data\Anbennar.json",
		//		JsonConvert.SerializeObject(
		//			new Anbennar { Countries = concatedCountries.ToList(), Bonuses = bonuses },
		//			Formatting.Indented,
		//			new JsonSerializerSettings { ContractResolver = contractResolver, NullValueHandling = NullValueHandling.Ignore }));
		//}

		static void GenerateAnbennarData()
		{
			var objLoader = new AnbennarObjLoader();
			var parser = new Parsing.EuObjParser();
			List<CountryTag> countryTags;
			List<Country> countries;
			List<CountryIdea> countryIdeas;
			List<Models.Base.ReligionGroup> religionGroups;
			List<Models.Base.ChurchAspect> churchAspects;

			using (var sr = objLoader.LoadFiles("country_tags", "anb*"))
			{
				countryTags = parser.DeserialiseEuObj<CountryTag>(sr);
			}
			using (var sr = objLoader.LoadFlatFiles("countries", matchExclude: new Regex("(?:Y[0-9]{2})|(?:Lot Dek)")))
			{
				countries = parser.DeserialiseEuObj<Country>(sr);
			}
			using (var sr = objLoader.LoadFiles("ideas", "anb*"))
			{
				countryIdeas = parser.DeserialiseEuObj<CountryIdea>(sr);
			}
			using(var sr = objLoader.LoadFile("religions", "anb_religion.txt"))
			{
				religionGroups = parser.DeserialiseEuObj<Models.Base.ReligionGroup>(sr);
			}
			using (var sr = objLoader.LoadFile("church_aspects", "anb_ancestor_worship.txt"))
			{
				churchAspects = parser.DeserialiseEuObj<Models.Base.ChurchAspect>(sr);
			}
			var fullCountries = CombineCountryData(countries, countryTags, countryIdeas);
			var fullReligions = CombineReligionData(religionGroups, churchAspects);
			var bonuses = countryIdeas.SelectMany(x => x.Ideas.SelectMany(x => x.Bonuses))
				.DistinctBy(x => x.Type).Select(x => x.Copy()).ToList();
			var anbennar = new Anbennar
			{
				Countries = fullCountries,
				ReligionGroups = fullReligions,
				Bonuses = bonuses,
			};
			DefaultContractResolver contractResolver = new DefaultContractResolver
			{
				NamingStrategy = new CamelCaseNamingStrategy()
			};
			var projectBaseFolder = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0];
			File.WriteAllText(@$"{projectBaseFolder}Web\Data\Anbennar.json",
				JsonConvert.SerializeObject(
					anbennar,
					Formatting.None,
					new JsonSerializerSettings { ContractResolver = contractResolver, NullValueHandling = NullValueHandling.Ignore }));
		}

		static void GenerateIdeaVariationData()
		{
			var objLoader = new IdeaVariationObjLoader();

			List<Models.IdeaGroup> ideaGroups;
			List<Models.Policy> policies;

			using (var sr = objLoader.LoadFiles("ideas"))
			{
				ideaGroups = new Parsing.EuObjParser().DeserialiseEuObj<Models.IdeaGroup>(sr);
			}
			using (var sr = objLoader.LoadFiles("policies", "Idea_Variation*"))
			{
				policies = new Parsing.EuObjParser().DeserialiseEuObj<Models.Policy>(sr);
			}
			var bonuses = policies.Select(x => x.Bonuses).SelectMany(x => x)
				.Concat(ideaGroups.SelectMany(x => x.Ideas.SelectMany(x => x.Bonuses)))
				.DistinctBy(x => x.Type).Select(x => x.Copy()).ToList();
			var ideaVariation = new IdeaVariation
			{
				Bonuses = bonuses,
				IdeaGroups = ideaGroups,
				Policies = policies
			};
			DefaultContractResolver contractResolver = new DefaultContractResolver
			{
				NamingStrategy = new CamelCaseNamingStrategy()
			};
			var projectBaseFolder = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0];
			File.WriteAllText(@$"{projectBaseFolder}Web\Data\IdeaVariation.json",
				JsonConvert.SerializeObject(
					ideaVariation,
					Formatting.None,
					new JsonSerializerSettings { ContractResolver = contractResolver, NullValueHandling = NullValueHandling.Ignore }));
		}

		static void Main(string[] args)
		{
			GenerateIdeaVariationData();
			GenerateAnbennarData();
			ProcessWeb();
			//File.WriteAllText(@"C:\Users\corey\Desktop\CoreyGrant.github.io\Data\Policies.json", policyJson);
			//File.WriteAllText(@"C:\Users\corey\Desktop\CoreyGrant.github.io\Data\Ideas.json", ideaGroupsJson);
			//var parser = new Parser();
			//var policies = parser.ParsePolicies(policyJson);
			//var ideas = parser.ParseIdeaGroups(ideaGroupsJson);
			//DefaultContractResolver contractResolver = new DefaultContractResolver
			//{
			//	NamingStrategy = new CamelCaseNamingStrategy()
			//};
			//var bonuses = policies.Select(x => x.Bonuses).SelectMany(x => x)
			//	.Concat(ideas.SelectMany(x => x.Ideas.SelectMany(x => x.Bonuses)))
			//	.DistinctBy(x => x.Type).Select(x => x.Copy()).ToList();
			//foreach(var bonus in bonuses)
			//{
			//	bonus.Value = null;
			//}
			//var duplicates = policies.GroupBy(x => x.DisplayName)
			//	.Where(x => x.Count() > 1)
			//	.Select(x => x.Key).ToList();
			//Console.Write(JsonConvert.SerializeObject(duplicates));
			//File.WriteAllText(@"C:\Users\corey\Desktop\CoreyGrant.github.io\Data\IdeaVariation.json",
			//	JsonConvert.SerializeObject(
			//		new IdeaVariation {Policies = policies, IdeaGroups = ideas, Bonuses = bonuses.ToList() },
			//		Formatting.None,
			//		new JsonSerializerSettings { ContractResolver = contractResolver, NullValueHandling = NullValueHandling.Ignore }));

		}

		static void ProcessWeb()
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = "/C npm run-script build",
				}
			};
			process.OutputDataReceived += (p, line) => Console.Write(line);
			process.Start();
			process.WaitForExit();
		}

		static List<FullReligionGroup> CombineReligionData(
			List<Models.Base.ReligionGroup> religionGroups, 
			List<Models.Base.ChurchAspect> churchAspects)
		{
			return religionGroups.Select(group => new FullReligionGroup
			{
				CanFormPersonalUnions = group.CanFormPersonalUnions,
				DefenderOfFaith = group.DefenderOfFaith,
				Name = group.Name,
				Religions = group.Religions.Select(religion => new FullReligion
				{
					Name = religion.Name,
					Color = religion.Color,
					Country = religion.Country,
					SecondaryCountry = religion.SecondaryCountry,
					Province = religion.Province,
					Blessings = religion.Blessings
						?.Select(x => churchAspects.Single(y => y.Name == x))
						?.ToList()
				}).ToList()
			}).ToList();
		}

		static List<FullCountry> CombineCountryData(
			List<Country> countries,
			List<CountryTag> countryTags,
			List<CountryIdea> countryIdeas)
		{
			var fullCountries = new List<FullCountry>();
			foreach (var idea in countryIdeas)
			{
				var tag = countryTags.SingleOrDefault(x => x.Tag == idea.CountryTag || x.Country.ToLower() == idea.CountryTag.ToLower());
				if (tag == null)
				{
					continue;
				}
				var country = countries.SingleOrDefault(x => x.Name == tag.Country);
				fullCountries.Add(new FullCountry
				{
					Name = tag.Country,
					Ideas = idea.Ideas,
					Colors = country == null 
						? new List<Color>()
						: new List<Models.Base.Color> { country.Color }
				});
			}
			return fullCountries;
		}
	}
}
