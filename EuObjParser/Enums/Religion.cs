using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EuObjParser.Enums
{
	enum Religion
	{
		[EuKeyAttribute("cannorian_pantheon")]
		[Display(Name = "Cannorian Pantheon")]
		cannorian_pantheon,
		[EuKeyAttribute("regent_court")]
		[Display(Name = "Regent Court")]
		regent_court,
		[EuKeyAttribute("corinite")]
		[Display(Name = "Corinite")]
		corinite,
		[EuKeyAttribute("ravelian")]
		[Display(Name = "Ravelian")]
		ravelian,
		[EuKeyAttribute("the_thought")]
		[Display(Name = "The Thought")]
		the_thought,
		[EuKeyAttribute("godlost")]
		[Display(Name = "Godlost")]
		godlost,
		[EuKeyAttribute("khetist")]
		[Display(Name = "Khetist")]
		khetist,
		[EuKeyAttribute("mother_akan")]
		[Display(Name = "Mother akan")]
		mother_akan,
		[EuKeyAttribute("xhazobkult")]
		[Display(Name = "Xhazobkult")]
		xhazobkult,
		[EuKeyAttribute("ancestor_worship")]
		[Display(Name = "Ancestor Worship")]
		ancestor_worship,
		[EuKeyAttribute("runefather_worship")]
		[Display(Name = "Runefather Worship")]
		runefather_worship,
		[EuKeyAttribute("elven_forebears")]
		[Display(Name = "Elven Forebears")]
		elven_forebears,
		[EuKeyAttribute("soise_vio")]
		[Display(Name = "Soise Vio")]
		soise_vio,
		[EuKeyAttribute("bulwari_sun_cult")]
		[Display(Name = "Bulwari Sun Cult")]
		bulwari_sun_cult,
		[EuKeyAttribute("old_bulwari_sun_cult")]
		[Display(Name = "Old Bulwari Sun Cult")]
		old_bulwari_sun_cult,
		[EuKeyAttribute("the_jadd")]
		[Display(Name = "The jadd")]
		the_jadd,
		[EuKeyAttribute("skaldhyrric_faith")]
		[Display(Name = "Skaldhyrric Faith")]
		skaldhyrric_faith,
		[EuKeyAttribute("great_dookan")]
		[Display(Name = "Great Dookan")]
		great_dookan,
		[EuKeyAttribute("old_dookan")]
		[Display(Name = "Old Dookan")]
		old_dookan,
		[EuKeyAttribute("kobold_dragon_cult")]
		[Display(Name = "Kobold dragon cult")]
		kobold_dragon_cult,
		[EuKeyAttribute("drozma_tur")]
		[Display(Name = "Drozma tur")]
		drozma_tur,
		[EuKeyAttribute("kobold_serpent_cult")]
		[Display(Name = "Kobold Serpent Cult")]
		kobold_serpent_cult,
		[EuKeyAttribute("goblinic_shamanism")]
		[Display(Name = "Goblinic Shamanism")]
		goblinic_shamanism,
		[EuKeyAttribute("the_hunt")]
		[Display(Name = "The hunt")]
		the_hunt,
		[EuKeyAttribute("weeping_mother")]
		[Display(Name = "Weeping mother")]
		weeping_mother,
		[EuKeyAttribute("death_cult_of_cheshosh")]
		[Display(Name = "Death cult of cheshosh")]
		death_cult_of_cheshosh,
		[EuKeyAttribute("kalun_masks")]
		[Display(Name = "Kalun masks")]
		kalun_masks,
		[EuKeyAttribute("tswohvwohii")]
		[Display(Name = "Tswohvwohii")]
		tswohvwohii,
		[EuKeyAttribute("chahinanito")]
		[Display(Name = "Chahinanito")]
		chahinanito,
		[EuKeyAttribute("song_servants")]
		[Display(Name = "Song Servants")]
		song_servants,
		[EuKeyAttribute("orwaii")]
		[Display(Name = "Orwaii")]
		orwaii,
		[EuKeyAttribute("leechfather")]
		[Display(Name = "Leechfather")]
		leechfather,
		[EuKeyAttribute("kheionism")]
		[Display(Name = "Kheionism")]
		kheionism,
		[EuKeyAttribute("gods_of_the_taychend")]
		[Display(Name = "Gods of the taychend")]
		gods_of_the_taychend,
		[EuKeyAttribute("ynn_river_worship")]
		[Display(Name = "Ynn river worship")]
		ynn_river_worship,
		[EuKeyAttribute("fey_court")]
		[Display(Name = "Fey court")]
		fey_court,
		[EuKeyAttribute("eordellon")]
		[Display(Name = "Eordellon")]
		eordellon,
		[EuKeyAttribute("spring_court")]
		[Display(Name = "Spring court")]
		spring_court,
		[EuKeyAttribute("summer_court")]
		[Display(Name = "Summer court")]
		summer_court,
		[EuKeyAttribute("autumn_court")]
		[Display(Name = "Autumn court")]
		autumn_court,
		[EuKeyAttribute("winter_court")]
		[Display(Name = "Winter court")]
		winter_court,
		[EuKeyAttribute("ashentree_pact")]
		[Display(Name = "Ashentree pact")]
		ashentree_pact,
		[EuKeyAttribute("religious_schools")]
		[Display(Name = "Religious schools")]
		religious_schools,
		[EuKeyAttribute("high_philosophy")]
		[Display(Name = "High philosophy")]
		high_philosophy,
		[EuKeyAttribute("righteous_path")]
		[Display(Name = "Righteous path")]
		righteous_path,
		[EuKeyAttribute("lefthand_path")]
		[Display(Name = "Lefthand path")]
		lefthand_path,
		[EuKeyAttribute("kodave_followers")]
		[Display(Name = "Kodave followers")]
		kodave_followers,
		[EuKeyAttribute("yukel_followers")]
		[Display(Name = "Yukel followers")]
		yukel_followers,
		[EuKeyAttribute("enuuk_followers")]
		[Display(Name = "Enuuk followers")]
		enuuk_followers,
		[EuKeyAttribute("kalyin_worshippers")]
		[Display(Name = "Kalyin worshippers")]
		kalyin_worshippers,
		[EuKeyAttribute("feast_of_the_gods")]
		[Display(Name = "Feast of the gods")]
		feast_of_the_gods,
		[EuKeyAttribute("mountain_watchers")]
		[Display(Name = "Mountain watchers")]
		mountain_watchers,
		[EuKeyAttribute("ik_magthaal")]
		[Display(Name = "Ik magthaal")]
		ik_magthaal,
	}
}
