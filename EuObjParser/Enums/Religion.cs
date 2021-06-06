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
		[Display(Name = "Cannorian pantheon")]
		cannorian_pantheon,
		[EuKeyAttribute("regent_court")]
		[Display(Name = "Regent court")]
		regent_court,
		[EuKeyAttribute("corinite")]
		[Display(Name = "Corinite")]
		corinite,
		[EuKeyAttribute("ravelian")]
		[Display(Name = "Ravelian")]
		ravelian,
		[EuKeyAttribute("the_thought")]
		[Display(Name = "The thought")]
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
		[Display(Name = "Ancestor worship")]
		ancestor_worship,
		[EuKeyAttribute("runefather_worship")]
		[Display(Name = "Runefather worship")]
		runefather_worship,
		[EuKeyAttribute("elven_forebears")]
		[Display(Name = "Elven forebears")]
		elven_forebears,
		[EuKeyAttribute("soise_vio")]
		[Display(Name = "Soise vio")]
		soise_vio,
		[EuKeyAttribute("bulwari_sun_cult")]
		[Display(Name = "Bulwari sun cult")]
		bulwari_sun_cult,
		[EuKeyAttribute("old_bulwari_sun_cult")]
		[Display(Name = "Old bulwari sun cult")]
		old_bulwari_sun_cult,
		[EuKeyAttribute("the_jadd")]
		[Display(Name = "The jadd")]
		the_jadd,
		[EuKeyAttribute("skaldhyrric_faith")]
		[Display(Name = "Skaldhyrric faith")]
		skaldhyrric_faith,
		[EuKeyAttribute("great_dookan")]
		[Display(Name = "Great dookan")]
		great_dookan,
		[EuKeyAttribute("old_dookan")]
		[Display(Name = "Old dookan")]
		old_dookan,
		[EuKeyAttribute("kobold_dragon_cult")]
		[Display(Name = "Kobold dragon cult")]
		kobold_dragon_cult,
		[EuKeyAttribute("drozma_tur")]
		[Display(Name = "Drozma tur")]
		drozma_tur,
		[EuKeyAttribute("kobold_serpent_cult")]
		[Display(Name = "Kobold serpent cult")]
		kobold_serpent_cult,
		[EuKeyAttribute("goblinic_shamanism")]
		[Display(Name = "Goblinic shamanism")]
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
		[Display(Name = "Song servants")]
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
		[EuKeyAttribute("totemism")]
		[Display(Name = "totemism")]
		totemism,
		[EuKeyAttribute("animism")]
		[Display(Name = "Animism")]
		animism,
		[EuKeyAttribute("catholic")]
		[Display(Name = "Catholic")]
		catholic,
		[EuKeyAttribute("anglican")]
		[Display(Name = "Anglican")]
		anglican,
		[EuKeyAttribute("hussite")]
		[Display(Name = "Hussite")]
		hussite,
		[EuKeyAttribute("protestant")]
		[Display(Name = "Protestant")]
		protestant,
		[EuKeyAttribute("reformed")]
		[Display(Name = "Reformed")]
		reformed,
		[EuKeyAttribute("orthodox")]
		[Display(Name = "Orthodox")]
		orthodox,
		[EuKeyAttribute("coptic")]
		[Display(Name = "Coptic")]
		coptic,
		[EuKeyAttribute("sunni")]
		[Display(Name = "Sunni")]
		sunni,
		[EuKeyAttribute("shiite")]
		[Display(Name = "Shiite")]
		shiite,
		[EuKeyAttribute("ibadi")]
		[Display(Name = "Ibadi")]
		ibadi,
		[EuKeyAttribute("buddhism")]
		[Display(Name = "Buddhism")]
		buddhism,
		[EuKeyAttribute("vajrayana")]
		[Display(Name = "Vajrayana")]
		vajrayana,
		[EuKeyAttribute("mahayana")]
		[Display(Name = "Mahayana")]
		mahayana,
		[EuKeyAttribute("confucianism")]
		[Display(Name = "Confucianism")]
		confucianism,
		[EuKeyAttribute("shinto")]
		[Display(Name = "Shinto")]
		shinto,
		[EuKeyAttribute("hinduism")]
		[Display(Name = "Hinduism")]
		hinduism,
		[EuKeyAttribute("sikhism")]
		[Display(Name = "Sikhism")]
		sikhism,
		[EuKeyAttribute("shamanism")]
		[Display(Name = "Shamanism")]
		shamanism,
		[EuKeyAttribute("inti")]
		[Display(Name = "Inti")]
		inti,
		[EuKeyAttribute("nahuatl")]
		[Display(Name = "Nahuatl")]
		nahuatl,
		[EuKeyAttribute("mesoamerican_religion")]
		[Display(Name = "Mesoamerican")]
		mesoamerican_religion,
		[EuKeyAttribute("norse_pagan_reformed")]
		[Display(Name = "Norse")]
		norse_pagan_reformed,
		[EuKeyAttribute("tengri_pagan_reformed")]
		[Display(Name = "Tengri")]
		tengri_pagan_reformed,
		[EuKeyAttribute("jewish")]
		[Display(Name = "Jewish")]
		jewish,
		[EuKeyAttribute("zoroastrian")]
		[Display(Name = "Zoroastrian")]
		zoroastrian
	}
}
