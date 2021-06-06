using EuObjParser.Models.Clauzwitz.greatProjects;
using EuObjParser.Models.Clauzwitz.shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Output.GreatProjects
{
	class GreatProject
	{
		public string Name { get; set; }
		public int Start { get; set; }
		public string StartName { get; set; }
		public int BuildCost { get; set; }
		public bool CanBeMoved { get; set; }
		public int StartingTier { get; set; }
		public string Type { get; set; }
		public Trigger BuildTrigger { get; set; }
		public Trigger CanUseModifiersTrigger { get; set; }
		public Trigger CanUpgradeTrigger { get; set; }
		public Trigger KeepTrigger { get; set; }
		public Tier Tier0 { get; set; }
		public Tier Tier1 { get; set; }
		public Tier Tier2 { get; set; }
		public Tier Tier3 { get; set; }
	}
}
