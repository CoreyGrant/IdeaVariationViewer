using EuObjParser.Attributes;
using EuObjParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Base
{
	class ChurchAspect
	{
		public string DisplayName => Helpers.GetName(Name);
		[EuObjPropName]
		public Enums.ChurchAspect Name { get; set; }
		[EuPropName("is_blessing")]
		public bool IsBlessing { get; set; }
		[EuPropName("modifier")]
		public List<Bonus> Bonuses { get; set; }
		[EuPropName("cost")]
		public int Cost { get; set; }
	}
}
