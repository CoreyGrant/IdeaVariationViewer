using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuObjParser.Models
{
	class IdeaVariation
	{
		public List<Policy> Policies { get; set; }
		public List<IdeaGroup> IdeaGroups { get; set; }
		public List<Bonus> Bonuses { get; set; }
		public Dictionary<string, int> ExclusiveCategories { get; set; } 
			= Enumerable.Range(0, 8).Select(x => (Enums.EuIdeaGroupCategory)x).ToDictionary(Helpers.GetName, x => (int)x);
	}
}
