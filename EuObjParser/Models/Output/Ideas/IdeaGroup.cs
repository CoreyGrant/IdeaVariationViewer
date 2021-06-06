using EuObjParser.Models.Clauzwitz.shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Output.Ideas
{
	class IdeaGroup
	{
		public string Name { get; set; }
		public string Category { get; set; }
		public List<Idea> Ideas { get; set; }
		public Trigger Trigger { get; set; }

		public string LocalizedName { get; set; }
		public string LocalizedDesc { get; set; }
	}

	class Idea
	{
		public string Name { get; set; }
		public Dictionary<string, string> Bonuses { get; set; }
		public string LocalizedName { get; set; }
		public string LocalizedDesc { get; set; }
	}
}
