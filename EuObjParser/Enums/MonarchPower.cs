using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EuObjParser.Enums
{
	enum MonarchPower
	{
		[EuKeyAttribute("ADM")]
		[Display(Name = "ADM")]
		Adm = 0,
		[EuKeyAttribute("DIP")]
		[Display(Name = "DIP")]
		Dip = 1,
		[EuKeyAttribute("MIL")]
		[Display(Name = "MIL")]
		Mil = 2
	}
}
