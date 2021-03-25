using EuObjParser.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models
{
	class Anbennar
	{
		public List<FullCountry> Countries { get; set; }
		public List<Bonus> Bonuses { get; set; }
		public List<FullReligionGroup> ReligionGroups { get; set; }
	}

	class FullCountry
	{
		public string Name { get; set; }
		public List<Idea> Ideas { get; set; }

		public List<Color> Colors { get; set; }
	}

	class FullReligionGroup
	{
		public Enums.ReligionGroup Name { get; set; }
		public string DisplayName => Helpers.GetName(Name);
		public bool DefenderOfFaith { get; set; }
		public bool CanFormPersonalUnions { get; set; }
		public List<FullReligion> Religions { get; set; }
	}

	class FullReligion
	{
		public Enums.Religion Name { get; set; }
		public string DisplayName => Helpers.GetName(Name);
		public Color Color { get; set; }
		public List<Bonus> Country { get; set; }
		public List<Bonus> SecondaryCountry { get; set; }
		public List<Bonus> Province { get; set; }
		public List<ChurchAspect> Blessings { get; set; }
	}
}
