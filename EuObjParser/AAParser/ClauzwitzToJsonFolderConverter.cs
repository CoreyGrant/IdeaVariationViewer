using EuObjParser.Config;
using EuObjParser.Parsing;
using System;
using System.IO;
using System.Linq;

namespace EuObjParser.AAParser
{
	class ClauzwitzToJsonFolderConverter
	{
		private const string SearchPattern = "*.txt";
		private readonly ClauzwitzToJsonConverter _converter;
		private readonly DataWriter _writer;
		private readonly ClauzwitzYamlLoader _yamlLoader;
		private readonly Eu4ModViewerConfig _config;

		public ClauzwitzToJsonFolderConverter(
			ClauzwitzToJsonConverter converter,
			DataWriter writer,
			Eu4ModViewerConfig config,
			ClauzwitzYamlLoader yamlLoader)
		{
			_converter = converter;
			_writer = writer;
			_config = config;
			_yamlLoader = yamlLoader;
		}

		public void Convert(string baseFolder, long modId)
		{
			var commonPath = Path.Combine(baseFolder, "common");
			var historyPath = Path.Combine(baseFolder, "history");
			var localizationPath = Path.Combine(baseFolder, "localisation");
			//var commonFiles = Directory.GetFiles(commonPath, SearchPattern);
			//var historyFiles = Directory.GetFiles(historyPath, SearchPattern);
			var commonDirectories = Directory.GetDirectories(commonPath);
			ConvertFilesInFolder(baseFolder, "common", modId);
			foreach (var dir in commonDirectories)
			{
				ConvertFilesInFolder(baseFolder, Path.Combine("common", dir.Split("\\").Last()), modId);
			}
			if (Directory.Exists(localizationPath))
			{
				var localizations = _yamlLoader.LoadLocalizations(localizationPath);
				_writer.Write("localisations", localizations, Path.Combine("Temp", modId.ToString()));
			}
			if (Directory.Exists(historyPath))
			{
				var historyDirectories = Directory.GetDirectories(historyPath);
				ConvertFilesInFolder(baseFolder, "history", modId);
				foreach (var dir in historyDirectories)
				{
					ConvertFilesInFolder(baseFolder, Path.Combine("history", dir.Split("\\").Last()), modId);
				}
			}
		}

		private void ConvertFilesInFolder(string folderPath, string folderName, long modId)
		{
			var folder = Path.Combine(folderPath, folderName);
			Console.WriteLine("Converting files in folder " + folder);
			var files = Directory.GetFiles(folder, SearchPattern).Select(x => x.Split("\\").Last());
			foreach(var file in files)
			{
				var obj = _converter.ParseObject(file, folder);
				_writer.WriteJObject(file.Split(".txt")[0], obj, Path.Combine("Temp", modId.ToString(), folderName));
			}
		}
	}
}
