using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EuObjParser
{
	public class AnbennarObjLoader : EuObjLoader
	{
		public AnbennarObjLoader()
		{
			FileLocation = @"C:\Program Files (x86)\Steam\steamapps\workshop\content\236850\2370830316\";
		}
	}
	
	public class IdeaVariationObjLoader : EuObjLoader
	{
		public IdeaVariationObjLoader()
		{
			FileLocation = @"C:\Program Files (x86)\Steam\steamapps\workshop\content\236850\604203692\";
		}
	}

	public class EuObjLoader
	{
		protected string FileLocation { get; set; }

		public StreamReader LoadFile(string folder, string fileName, string mainFolder = "common")
		{
			return File.OpenText(FileLocation + mainFolder + "\\" + folder + "\\" + fileName);
		}

		public StreamReader LoadFiles(string folder, string searchPattern = "*", string mainFolder = "common")
		{
			return CombineFiles(Directory.GetFiles(FileLocation + mainFolder + "\\" + folder, searchPattern).ToList()
				.Select(File.ReadAllText).ToList());
		}

		public StreamReader LoadFlatFiles(string folder, string searchPattern = "*", Regex matchExclude = null, string mainFolder = "common")
		{
			var fileList = Directory.GetFiles(FileLocation + mainFolder + "\\" + folder, "*").ToList();
			if(matchExclude != null)
			{
				fileList = fileList.Where(x => !matchExclude.IsMatch(x)).ToList();
			}
			return FlattenFiles(fileList.ToDictionary(x => x, x => File.ReadAllText(x)));
		}

		private StreamReader FlattenFiles(Dictionary<string, string> files)
		{
			var oneBigFile = "";
			foreach(var kv in files)
			{
				oneBigFile += @$"{kv.Key.Split("\\").Last()} = {{{Environment.NewLine}{kv.Value}{Environment.NewLine}}}{Environment.NewLine}";
			}
			return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(oneBigFile)));
		}

		private StreamReader CombineFiles(List<string> strings)
		{
			var oneBigFile = string.Join("\n", strings);
			return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(oneBigFile)));
		}
	}
}
