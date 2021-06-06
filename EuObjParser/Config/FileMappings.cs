using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Config
{
	class FileMappings
	{
		//public FileMapping AdvisorTypes { get; set; }
		//public FileMapping Ages { get; set; }

		//public FileMapping Buildings { get; set; }
		//public FileMapping CbTypes { get; set; }
		public FileMappingsSections Sections { get; set; }
		public FileMapping Countries { get; set; }
		public FileMapping CountryTags { get; set; }

		public FileMapping Cultures { get; set; }
		public FileMapping GreatProjects { get; set; }
		public FileMapping Ideas { get; set; }
		public FileMapping Policies { get; set; }
		public FileMapping Religions { get; set; }
		public FileMapping ChurchAspects { get; set; }
		public FileMapping AdvisorTypes { get; set; }

		public HistoryFileMappings History { get; set; }

		public bool CollapsePoliciesByAge { get; set; }
	}

	class HistoryFileMappings
	{
		public FileMapping Countries { get; set; }
		public FileMapping Provinces { get; set; }
	}

	class FileMappingsSections
	{
		public bool Countries { get; set; }
		public bool GreatProjects { get; set; }
		public bool Ideas { get; set; }
		public bool Policies { get; set; }
		public bool Religions { get; set; }
		public bool Advisors { get; set; }
	}

	class FileMapping
	{
		/// <summary>
		/// If true, will use all files.
		/// </summary>
		public bool All { get; set; }

		/// <summary>
		/// If true, will just use the base files.
		/// </summary>
		public bool Base { get; set; }

		/// <summary>
		/// If true, will add the names of the files loaded by the base game
		/// to the file list.
		/// 
		/// Useful where the changed data is directly in a copy of the base game file.
		/// </summary>
		public bool BaseNames { get; set; }

		/// <summary>
		/// If true, will collapse items with the base game items.
		/// 
		/// Needed when the mod adds some files, but uses the base game for most of them
		/// E.g. countries folder often just has the new tags added, but the mod still
		/// uses tags from the base game.
		/// </summary>
		public bool Collapse { get; set; }

		/// <summary>
		/// The files to include.
		/// </summary>
		public List<string> Files { get; set; }
		/// <summary>
		/// The files to ignore.
		/// </summary>
		public List<string> Except { get; set; }
	}
}
