using EuObjParser.Models.Clauzwitz.shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Output.Policies
{
	class Policy
	{
		public string Name { get; set; }
		public string MonarchPower { get; set; }
		public Trigger Potential { get; set; }
		public Trigger Allow { get; set; }
		public IReadOnlyDictionary<string, string> Bonuses { get; set; }
	}
}
