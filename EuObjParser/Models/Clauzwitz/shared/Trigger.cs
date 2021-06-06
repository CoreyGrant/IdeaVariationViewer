using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.shared
{
	/// <summary>
	/// The trigger is made up of one or more condition sets.
	/// </summary>
	class Trigger
	{
		public List<TriggerCondition> Conditions { get; set; }
		public List<TriggerConditionSet> ConditionSets { get; set; }
	}

	class TriggerConditionSet
	{
		public bool ComposeOr { get; set; }
		public bool ModifierNot { get; set; }
		public List<TriggerCondition> Conditions { get; set; }
		public List<TriggerConditionSet> ConditionSets { get; set; }
	}

	class TriggerCondition
	{
		public string Name { get; set; }
		public string Value { get; set; }
	}
}
