using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.ideas
{
	class IdeaGroup
	{
		[PropertyName]
		public string Name { get; set; }
		public bool Free { get; set; }
		public string Category { get; set; }
		public Trigger Trigger { get; set; }
		[Remaining("ai_will_do")]
		public IReadOnlyCollection<Idea> Ideas { get; set; }
	}

	/// <summary>
	/// An idea in an idea group.
	/// Some of the idea names are the same, if they are one of the 
	/// Bonuses list will have values, the others won't, but they all use the 
	/// one with bonuses.
	/// i.e. Ideas are the same if they have the same name, but always have bonuses
	/// </summary>
	class Idea
	{
		[PropertyName]
		public string Name { get; set; }
		[Remaining]
		public IReadOnlyDictionary<string, string> Bonuses { get; set; }
	}
}
