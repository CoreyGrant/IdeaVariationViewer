using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EuObjParser.Enums
{
    enum IdeaGroup
    {
        [EuKeyAttribute("administrative_ideas")] 
        Administrative,
        [EuKeyAttribute("innovativeness_ideas")] 
        Innovativeness,
        [EuKeyAttribute("religious_ideas")]
        Religious,
        [EuKeyAttribute("spy_ideas")]
        Espionage,
        [EuKeyAttribute("dynasty0")]
        Dynastic,
        [EuKeyAttribute("influence_ideas")]
        Influence,
        [EuKeyAttribute("offensive_ideas")]
        Offensive,
        [EuKeyAttribute("defensive_ideas")]
        Defensive,
        [EuKeyAttribute("trade_ideas")]
        Trade,
        [EuKeyAttribute("economic_ideas")]
        Economic,
        [EuKeyAttribute("exploration_ideas")]
        Exploration,
        [EuKeyAttribute("maritime_ideas")]
        Maritime,
        [EuKeyAttribute("naval_ideas")]
        Naval,
        [EuKeyAttribute("quality_ideas")]
        Quality,
        [EuKeyAttribute("quantity_ideas")]
        Quantity,
        [EuKeyAttribute("aristocracy_ideas")]
        Aristocracy,
        [EuKeyAttribute("plutocracy_ideas")]
        Plutocracy,
        [EuKeyAttribute("diplomatic_ideas")]
        Diplomatic,
        [EuKeyAttribute("expansion_ideas")]
        Expansion,
        [EuKeyAttribute("humanist_ideas")]
        Humanist,
        [EuKeyAttribute("katholisch0")] 
        Catholic,
        [EuKeyAttribute("protestant0")]
        Protestant,
        [EuKeyAttribute("reformiert0")] 
        Reformed,
        [EuKeyAttribute("orthodox0")]
        Orthodox,
        [EuKeyAttribute("islam0")]
        Sunni,
        [EuKeyAttribute("tengri0")]
        Tengri,
        [EuKeyAttribute("hindu0")]
        Hindu,
        [EuKeyAttribute("confuci0")]
        Confucian,
        [EuKeyAttribute("budda0")] 
        Buddhist,
        [EuKeyAttribute("norse0")] 
        Norse,
        [EuKeyAttribute("shinto0")]
        Shinto,
        [EuKeyAttribute("cathar0")] 
        Cathar,
        [EuKeyAttribute("coptic0")]
        Coptic,
        [EuKeyAttribute("romuva0")] 
        Romuva,
        [EuKeyAttribute("suomi0")]
        Suomi,
        [EuKeyAttribute("jew0")] 
        Jewish,
        [EuKeyAttribute("slav0")] 
        Slav,
        [EuKeyAttribute("helle0")]
        Hellanistic,
        [EuKeyAttribute("mane0")]
        Manichean,
        [EuKeyAttribute("animist0")] 
        Animist,
        [EuKeyAttribute("feti0")] 
        Fetishist,
        [EuKeyAttribute("zoro0")] 
        Zoroastrianism,
        [EuKeyAttribute("ancli0")] 
        Anglican,
        [EuKeyAttribute("nahu0")]
        Nahuatl,
        [EuKeyAttribute("mesoam0")] 
        Mesoamerican,
        [EuKeyAttribute("inti0")] 
        Inti,
        [EuKeyAttribute("tote0")] 
        Totemism,
        [EuKeyAttribute("shia0")] 
        Shia,
        [EuKeyAttribute("ibadi0")] 
        Ibadi,
        [EuKeyAttribute("hussite0")]
        Hussite,
        [EuKeyAttribute("justiz0")] 
        Judicial,
        [EuKeyAttribute("entwicklung0")] 
        Development,
        [EuKeyAttribute("gesundheit0")]
        Health,
        [EuKeyAttribute("monarchie0")]
        Monarchy,
        [EuKeyAttribute("republik0")] 
        Republic,
        [EuKeyAttribute("aristo0")] 
        Theocracy,
        [EuKeyAttribute("diktatur0")] 
        Dictatorship,
        [EuKeyAttribute("horde0")]
        Horde,
        [EuKeyAttribute("gross0")]
        [Display(Name = "Heavy ship")]
        HeavyShip,
        [EuKeyAttribute("galle0")] 
        Galley,
        [EuKeyAttribute("handel0")]
        [Display(Name = "Light ship")]
        LightShip,
        [EuKeyAttribute("kolonialimperium0")]
        [Display(Name = "Colonial empire")]
        ColonialEmpire,
        [EuKeyAttribute("assimilation0")] 
        Assimilation,
        [EuKeyAttribute("gesellschaft0")]
        Society,
        [EuKeyAttribute("propaganda0")]
        Propaganda,
        [EuKeyAttribute("flottenbasis0")]
        [Display(Name = "Fleet base")]
        FleetBase,
        [EuKeyAttribute("nationalismus0")] 
        Nationalism,
        [EuKeyAttribute("konigreich0")]
        [Display(Name = "Imperial ambition")]
        ImperialAmbition,
        [EuKeyAttribute("imperialismus0")]
        Imperial,
        [EuKeyAttribute("generalstab0")]
        [Display(Name = "General staff")]
        GeneralStaff,
        [EuKeyAttribute("stehendesheer0")]
        [Display(Name = "Standing army")]
        StandingArmy,
        [EuKeyAttribute("soldnerheer0")]
        [Display(Name = "Mercenary army")]
        MercenaryArmy,
        [EuKeyAttribute("wehrpflicht0")] 
        Conscription,
        [EuKeyAttribute("waffenqualitat0")]
        [Display(Name = "Weapon quality")]
        WeaponQuality,
        [EuKeyAttribute("festung0")] 
        Fortress,
        [EuKeyAttribute("kriegsproduktion0")]
        [Display(Name = "War production")]
        WarProduction,
        [EuKeyAttribute("staatsverwaltung0")]
        [Display(Name = "State administration")]
        StateAdministration,
        [EuKeyAttribute("zentra0")] 
        Centralism,
        [EuKeyAttribute("dezentra0")] 
        Decentralism,
        [EuKeyAttribute("formation0")] 
        Tactics,
        [EuKeyAttribute("militarismus0")]
        Militarism,
        [EuKeyAttribute("shock0")]
        Shock,
        [EuKeyAttribute("fire0")]
        Fire,
	}

    enum EuIdeaGroupCategory
    {
        General = 0,
        Government = 1,
        Damage = 2,
        Army = 3,
        Religion = 4,
        Centralisation = 5,
        Imperial = 6,
        Ship = 7,
        Muslim,
        [Display(Name = "Non republic")]
        NonRepublic,
        [Display(Name = "Non light ship")]
        NonLightShip,
        [Display(Name = "Non heavy ship")]
        NonHeavyShip,
        [Display(Name = "Non mercenary")]
        NonMerc,
        [Display(Name = "Any but shia/ibadi/hussite")]
        NotShiaIbadiHussite,
        [Display(Name = "Some religion")]
        SomeReligion,
        Any
    }

    enum EuIdeaGroupExtraCategory
	{
        
	}
}
