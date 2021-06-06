using EuObjParser.Models.Clauzwitz.religions;
using EuObjParser.Models.Clauzwitz.shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Output.Religions
{
	class ReligionGroup
	{
		public string Name { get; set; }
		public bool DefenderOfFaith { get; set; }
		public bool CanFormPersonalUnions { get; set; }
		public List<Religion> Religions { get; set; }
	}

	class Religion
	{
		public string Name { get; set; }
		public Color Color { get; set; }
		public IReadOnlyDictionary<string, string> Country { get; set; }
		public IReadOnlyDictionary<string, string> SecondaryCountry { get; set; }
		public IReadOnlyDictionary<string, string> Province { get; set; }
		public IReadOnlyCollection<ChurchAspect> Blessings { get; set; }
		public IReadOnlyCollection<ChurchAspect> Aspects { get; set; }
	}
}
