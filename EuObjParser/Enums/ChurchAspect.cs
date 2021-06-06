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
		[EuKeyAttribute("legitimize_government")]
		[Display(Name = "Legitimize government")]
		legitimize_government,
		[EuKeyAttribute("encourage_warriors_of_the_faith")]
		[Display(Name = "Encourage warriors of the faith")]
		encourage_warriors_of_the_faith,
		[EuKeyAttribute("send_monks_to_establish_monasteries")]
		[Display(Name = "Send monks to establish monasteries")]
		send_monks_to_establish_monasteries,
		[EuKeyAttribute("promote_territorial_rights")]
		[Display(Name = "Promote territorial rights")]
		promote_territorial_rights,
		[EuKeyAttribute("will_of_the_martyrs")]
		[Display(Name = "Will of the martyrs")]
		will_of_the_martyrs,
	}
}
