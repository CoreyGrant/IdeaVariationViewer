using EuObjParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using EuObjParser.Attributes;
using System.Text;

namespace EuObjParser.Models
{
	class Policy
	{
		public string DisplayName => GetDisplayName();
		[EuObjPropName]
		public string Name { get; set; }
		[EuPropName("monarch_power")]
		public MonarchPower MonarchPower { get; set; }
		public string MonarchPowerUrl => Helpers.GetImageUrl(MonarchPower);
		public string MonarchPowerName => Helpers.GetName(MonarchPower);
		[EuPropNameRemaining("ai_will_do")]
		public List<Bonus> Bonuses { get; set; }
		[EuPropName("potential")]
		public PolicyPotential Potential { get; set; }
		[EuPropName("allow")]
		public PolicyAllow Allow { get; set; }

		private string GetDisplayName()
		{
			var ideaGroupNames = new List<string>();
			if (Allow.Full?.Any() ?? false)
			{
				ideaGroupNames.AddRange(Allow.Full.Select(Helpers.GetName));
			}
			//if (Allow.FullAny?.Any() ?? false)
			//{
			//	var cat = Helpers.GetCompleteCategory(Allow.FullAny);
			//	if((int)cat == 1000) { 
			//		throw new Exception(); 
			//	}
			//	ideaGroupNames.Add($"{{{Helpers.GetName(cat)}}}");
			//}
			//if (Allow.HiddenTrigger?.Any() ?? false)
			//{
			//	var cat = Helpers.GetCompleteCategory(Allow.HiddenTrigger);
			//	if ((int)cat == 1000)
			//	{
			//		throw new Exception();
			//	}
			//	ideaGroupNames.Add($"{{{Helpers.GetName(cat)}}}");
			//}
			return string.Join(" + ", ideaGroupNames);
		}
	}

	class PolicyPotential
	{
		[EuObjList("has_idea_group")]
		public List<Enums.IdeaGroup> Has { get; set; }
		[EuObjList("OR")]
		public List<HasAnyIdeaGroup> HasAny { get; set; }
	}

	class HasAnyIdeaGroup
	{
		[EuObjList("has_idea_group")]
		public List<Enums.IdeaGroup> IdeaGroups { get; set; }
	}

	class FullAnyIdeaGroup
	{
		[EuObjList("full_idea_group")]
		public List<Enums.IdeaGroup> IdeaGroups { get; set; }
	}

	class PolicyAllow
	{
		[EuObjList("full_idea_group")]
		public List<Enums.IdeaGroup> Full { get; set; }
		[EuObjList("OR")]
		public List<FullAnyIdeaGroup> FullAny { get; set; }
		[EuObjCollapse("hidden_trigger,OR")]
		[EuObjList("has_idea_group")]
		public List<Enums.IdeaGroup> HiddenTrigger { get; set; }
		[EuObjCollapse("NOT")]
		[EuPropName("calc_true_if")]
		public PolicyAllowLimit PolicyAllowLimit { get; set; }
		[EuPropName("current_age")]
		public string CurrentAge { get; set; }
	}

	class PolicyAllowLimit
	{
		[EuPropName("amount")]
		public int Amount { get; set; }
		[EuObjList("has_active_policy")]
		public List<string> Policies { get; set; }
	}
}
