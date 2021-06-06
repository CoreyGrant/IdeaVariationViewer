using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.policies
{
	class Policy
	{
		public string MonarchPower { get; set; }
		public Trigger Potential { get; set; }
		public Trigger Allow { get; set; }
		[Remaining("ai_will_do")]
		public IReadOnlyDictionary<string, string> Bonuses { get; set; }
	}
}
