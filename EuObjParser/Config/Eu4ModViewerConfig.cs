using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Config
{
	class Eu4ModViewerConfig
	{
		public string BaseGamePath { get; set; }
		public string ModFolderPath { get; set; }
		public FileMappings BaseFileMappings { get; set; }
		public List<Mod> Mods { get; set; }
	}

	class Mod
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public FileMappings FileMappings { get; set; }
	}
}
