using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EuObjParser.Enums
{
	enum ReligionGroup
	{
		[EuKeyAttribute("cannorian")]
		[Display(Name = "Cannorian")]
		cannorian,
		[EuKeyAttribute("faithless")]
		[Display(Name = "Faithless")]
		faithless,
		[EuKeyAttribute("kheteratan")]
		[Display(Name = "Kheteratan")]
		kheteratan,
		[EuKeyAttribute("gnollish")]
		[Display(Name = "Gnollish")]
		gnollish,
		[EuKeyAttribute("dwarven")]
		[Display(Name = "Dwarven")]
		dwarven,
		[EuKeyAttribute("elven")]
		[Display(Name = "Elven")]
		elven,
		[EuKeyAttribute("bulwari")]
		[Display(Name = "Bulwari")]
		bulwari,
		[EuKeyAttribute("gerudian")]
		[Display(Name = "Gerudian")]
		gerudian,
		[EuKeyAttribute("orcish")]
		[Display(Name = "Orcish")]
		orcish,
		[EuKeyAttribute("dragon_cult")]
		[Display(Name = "Dragon cult")]
		dragon_cult,
		[EuKeyAttribute("goblin")]
		[Display(Name = "Goblin")]
		goblin,
		[EuKeyAttribute("harpy_cults")]
		[Display(Name = "Harpy cults")]
		harpy_cults,
		[EuKeyAttribute("aelantiri")]
		[Display(Name = "Aelantiri")]
		aelantiri,
		[EuKeyAttribute("harafe")]
		[Display(Name = "Harafe")]
		harafe,
		[EuKeyAttribute("effelai")]
		[Display(Name = "Effelai")]
		effelai,
		[EuKeyAttribute("andic")]
		[Display(Name = "Andic")]
		andic,
		[EuKeyAttribute("taychendi")]
		[Display(Name = "Taychendi")]
		taychendi,
		[EuKeyAttribute("ynnic")]
		[Display(Name = "Ynnic")]
		ynnic,
		[EuKeyAttribute("fey_religion")]
		[Display(Name = "Fey religion")]
		fey_religion,
		[EuKeyAttribute("raheni")]
		[Display(Name = "Raheni")]
		raheni,
		[EuKeyAttribute("halessi")]
		[Display(Name = "Halessi")]
		halessi,
		[EuKeyAttribute("triunic_lake")]
		[Display(Name = "Triunic lake")]
		triunic_lake,
		[EuKeyAttribute("giantkin")]
		[Display(Name = "Giantkin")]
		giantkin,
		[EuKeyAttribute("centaur_religion")]
		[Display(Name = "Centaur Religion")]
		centaur_religion,
	}
}
