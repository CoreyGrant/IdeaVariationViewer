using EuObjParser.Attributes;
using EuObjParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models
{
	class IdeaGroup
	{
		public Enums.EuIdeaGroupCategory ExclusiveCategory => Helpers.GetExclusiveCategory(Type);
		public string ExclusiveCategoryName => Helpers.GetName(ExclusiveCategory);

		public string CategoryUrl => Helpers.GetImageUrl(Category);
		[EuObjPropName]
		public Enums.IdeaGroup Type { get; private set; }
		public string TypeName => Helpers.GetName(Type);
		[EuPropName("category")]
		public MonarchPower Category { get; private set; }
		[EuPropNameRemaining("ai_will_do")]
		public List<Idea> Ideas { get; private set; }
		[EuPropName("trigger")]
		public IdeaGroupTrigger Trigger { get; private set; }
		[EuPropName("important")]
		public bool Important { get; private set; }
	}

	class Idea
	{
		[EuObjPropName]
		public string Name { get; set; }
		[EuIndex]
		public int Order { get; set; }
		[EuObjPropValue]
		public List<Bonus> Bonuses { get; set; }
	}

	class IdeaGroupTrigger
	{
		public string HasGovernmentAttribute { get; set; }
		public bool Primatives { get; set; } = true;
		public List<string> Religions { get; set; }
		public List<string> ReligionGroups { get; set; }
		public List<Enums.IdeaGroup> NotAnyIdeas { get; set; }

		public List<string> NotAnyReligions { get; set; }
		public List<string> NotAnyReligionGroups { get; set; }
		public IdeaGroupTriggerGovernmentRank GovernmentRank { get; set; }
	}

	class IdeaGroupTriggerGovernmentRank
	{
		public bool Emperor { get; set; }
	}
}
