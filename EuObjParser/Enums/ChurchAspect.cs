using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EuObjParser.Enums
{
	enum ChurchAspect
	{
		[EuKeyAttribute("global_unrest_dwarf")]
		[Display(Name = "Global unrest")]
		global_unrest_dwarf,
		[EuKeyAttribute("dev_cost_dwarf")]
		[Display(Name = "Dev cost")]
		dev_cost_dwarf,
		[EuKeyAttribute("tolerance_own_dwarf")]
		[Display(Name = "Tolerance of true faith")]
		tolerance_own_dwarf,
		[EuKeyAttribute("garrison_size_dwarf")]
		[Display(Name = "Garrison size")]
		garrison_size_dwarf,
		[EuKeyAttribute("build_cost_dwarf")]
		[Display(Name = "Build cost")]
		build_cost_dwarf,
		[EuKeyAttribute("morale_dwarf")]
		[Display(Name = "Morale")]
		morale_dwarf,
		[EuKeyAttribute("institution_spread_dwarf")]
		[Display(Name = "Institution spread")]
		institution_spread_dwarf,
		[EuKeyAttribute("global_colonial_growth_dwarf")]
		[Display(Name = "Global colonial growth")]
		global_colonial_growth_dwarf,
	}
}
