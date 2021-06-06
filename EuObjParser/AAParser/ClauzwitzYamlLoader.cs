﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EuObjParser.AAParser
{
	class ClauzwitzYamlLoader
	{
		private readonly IDeserializer _deserializer;

		public ClauzwitzYamlLoader()
		{
			_deserializer = new DeserializerBuilder()
				.WithNamingConvention(UnderscoredNamingConvention.Instance)
				.Build();
		}

		public IReadOnlyDictionary<string, string> LoadLocalizations(string folder, string searchPattern = "*l_english*")
		{
			var totalDict = new Dictionary<string, string>();
			var localizationFiles = Directory.GetFiles(folder, searchPattern);
			foreach (var file in localizationFiles)
			{
				var fileText = File.ReadAllText(file);
				var fileLines = fileText.Split('\n')
					.Select(x => x.Trim())
					.Where(x => !x.StartsWith("#"))
					.Where(x => !x.StartsWith("l_english:"))
					.Where(x => x.Contains(":"));
				foreach(var line in fileLines)
				{
					var firstColon = line.IndexOf(":");
					var name = line.Substring(0, firstColon);
					// Skip the colon, the weird number and the space
					var remaining = line.Substring(firstColon + 3);
					remaining = remaining.Split('#')[0];
					totalDict[name.ToLower()] = remaining.Replace("\"", "");
				}
				//var loc = _deserializer.Deserialize<Localization>(File.ReadAllText(file));
				//foreach (var kv in loc.l_english)
				//{
				//	totalDict[kv.Key] = kv.Value;
				//}
			}
			return totalDict;
		}
	}

	class Localization
	{
		public Dictionary<string, string> l_english { get; set; }
	}
}
