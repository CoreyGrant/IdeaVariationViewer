using EuObjParser.AAParser;
using EuObjParser.Config;
using EuObjParser.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EuObjParser.Parsing
{
	//class Eu4ModProcessor
	//{
	//	private readonly Eu4ModViewerConfig _config;
	//	private readonly Parser _parser;
	//	private readonly DataWriter _dataWriter;

	//	public Eu4ModProcessor(
	//		Eu4ModViewerConfig config,
	//		Parser parser,
	//		DataWriter dataWriter)
	//	{
	//		_config = config;
	//		_parser = parser;
	//		_dataWriter = dataWriter;
	//	}

	//	public void Process()
	//	{
	//		new FileMappingLoader(_config, _parser, _dataWriter).LoadFileMappings();
	//	}
	//}

	class ModList
	{
		public const int BaseGameId = 0;
		public List<Mod> Mods { get; set; }
	}

	class Mod
	{
		public string Name { get; set; }
		public long Id { get; set; }
		public List<ModSection> Sections { get; set; }
		public string Bonuses { get; set; }
	}

	class ModSection
	{
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public bool Unique { get; set; }
		public bool SomeBase { get; set; }
		public bool AllBase { get; set; }
	}
}
