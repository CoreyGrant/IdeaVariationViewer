using EuObjParser.AAParser;
using EuObjParser.Config;
using EuObjParser.Models.Clauzwitz.ideas;
using EuObjParser.Parsing;
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
		static void Main(string[] args)
		{
			Console.WriteLine("Rebuild game json? (y/n default n):");
			var rebuildGameJson = Console.ReadLine();
			Console.WriteLine("Rebuild output json? (y/n default n):");
			var rebuildOutputJson = Console.ReadLine();
			Console.WriteLine("Production javascript? (y/n default n):");
			var prodJs = Console.ReadLine();
			if (rebuildOutputJson.ToLower() == "y")
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				var ignore = new JsonConverterIgnore
				{
					Global = new[] { "ai_will_do", "male_names", "female_names", "dynasty_names" },
				};
				var converter = new ClauzwitzToJsonConverter(ignore);
				var config = GetConfig();
				var folderConverter = new ClauzwitzToJsonFolderConverter(converter, new DataWriter(), config, new ClauzwitzYamlLoader());
				var processor = new ClauzwitzJsonProcessor(new DataWriter(), config, folderConverter, new ClauzwitzJsonLoader());
				processor.Process(rebuildGameJson.ToLower() == "y");
			}
			ProcessWeb(prodJs.ToLower() == "y");
		}

		static void ProcessWeb(bool production = true)
		{
			var prodString = production ? "-prod" : "";
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = $"/C npm run-script build{prodString}",
				}
			};
			process.OutputDataReceived += (p, line) => Console.Write(line);
			process.Start();
			process.WaitForExit();
		}

		static Eu4ModViewerConfig GetConfig()
		{
			return JsonConvert.DeserializeObject<Eu4ModViewerConfig>(
				File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0], "eu4modviewer.json")));
		}
	}
}
