using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EuObjParser.Enums
{
	enum Bonus
	{
        [EuKeyAttribute("add_building")]
        [Display(Name = "Add building")]
        [EuValueType(BonusDisplayType.YesNo)]
        add_building,
    
        [EuKeyAttribute("development_cost_over_25")]
        [Display(Name = "Devepment cost for provinces over 25 dev")]
        [EuValueType(BonusDisplayType.YesNo)]
        development_cost_over_25,
    
        [EuKeyAttribute("add_age_forcelimit_naval")]
        [Display(Name = "Add naval force limit per age")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        add_age_forcelimit_naval,
    
        [EuKeyAttribute("add_age_authority")]
        [Display(Name = "Add mandate/IA per age")]
        [EuValueType(BonusDisplayType.OneDp)]
        add_age_authority,
    
        [EuKeyAttribute("extra_navytradition_heavy_ships")]
        [Display(Name = "Extra navy tradition from heavy ships")]
        [EuValueType(BonusDisplayType.YesNo)]
        extra_navytradition_heavy_ships,
    
        [EuKeyAttribute("extra_navytradition_galley")]
        [Display(Name = "Extra navy tradition from galleys")]
        [EuValueType(BonusDisplayType.YesNo)]
        extra_navytradition_galley,
    
        [EuKeyAttribute("extra_navytradition_light_ships")]
        [Display(Name = "Extra navy tradition from light ships")]
        [EuValueType(BonusDisplayType.YesNo)]
        extra_navytradition_light_ships,
    
        [EuKeyAttribute("reduced_dev_malus")]
        [Display(Name = "Reduced dev cost malus from nation size")]
        [EuValueType(BonusDisplayType.YesNo)]
        reduced_dev_malus,
    
        [EuKeyAttribute("free_cardinal")]
        [Display(Name = "Cardinal")]
        [EuValueType(BonusDisplayType.YesNo)]
        free_cardinal,
    
        [EuKeyAttribute("build_cost_in_subject_nation")]
        [Display(Name = "Build cost in subject nation")]
        [EuValueType(BonusDisplayType.Percentage)]
        build_cost_in_subject_nation,
    
        [EuKeyAttribute("stand_interaktion")]
        [Display(Name = "Estate interaction")]
        [EuValueType(BonusDisplayType.YesNo)]
        stand_interaktion,
  
        [EuKeyAttribute("add_age_manpower")]
        [Display(Name = "Manpower per age")]
        [EuValueType(BonusDisplayType.Thousand)]
        add_age_manpower,
  
        [EuKeyAttribute("add_age_sailors")]
        [Display(Name = "Sailors per age")]
        [EuValueType(BonusDisplayType.Thousand)]
        add_age_sailors,
  
        [EuKeyAttribute("add_cb")]
        [Display(Name = "Add CB")]
        [EuValueType(BonusDisplayType.Cb)]
        add_cb,
  
        [EuKeyAttribute("adm_tech_cost_modifier")]
        [Display(Name = "Admin tech cost modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        adm_tech_cost_modifier,
  
        [EuKeyAttribute("admiral_cost")]
        [Display(Name = "Admiral cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        admiral_cost,
  
        [EuKeyAttribute("advisor_cost")]
        [Display(Name = "Advisor cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        advisor_cost,
  
        [EuKeyAttribute("advisor_pool")]
        [Display(Name = "Possible advisors")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        advisor_pool,
  
        [EuKeyAttribute("ae_impact")]
        [Display(Name = "Aggressive expansion impact")]
        [EuValueType(BonusDisplayType.Percentage)]
        ae_impact,
  
        [EuKeyAttribute("allowed_marine_fraction")]
        [Display(Name = "Marine fraction")]
        [EuValueType(BonusDisplayType.Percentage)]
        allowed_marine_fraction,
  
        [EuKeyAttribute("army_tradition")]
        [Display(Name = "Yearly army tradition")]
        [EuValueType(BonusDisplayType.OneDp)]
        army_tradition,
  
        [EuKeyAttribute("army_tradition_decay")]
        [Display(Name = "Yearly army tradition decay")]
        [EuValueType(BonusDisplayType.Percentage)]
        army_tradition_decay,
  
        [EuKeyAttribute("artillery_bonus_vs_fort")]
        [Display(Name = "Artillery bonus vs fort")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        artillery_bonus_vs_fort,
  
        [EuKeyAttribute("artillery_cost")]
        [Display(Name = "Artillery cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        artillery_cost,
  
        [EuKeyAttribute("artillery_power")]
        [Display(Name = "Artillery combat ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        artillery_power,
  
        [EuKeyAttribute("autonomy_change_time")]
        [Display(Name = "Autonomy change cooldown")]
        [EuValueType(BonusDisplayType.Percentage)]
        autonomy_change_time,
  
        [EuKeyAttribute("backrow_artillery_damage")]
        [Display(Name = "Backrow artillery damage")]
        [EuValueType(BonusDisplayType.Percentage)]
        backrow_artillery_damage,
  
        [EuKeyAttribute("blockade_efficiency")]
        [Display(Name = "Blockade efficiency")]
        [EuValueType(BonusDisplayType.Percentage)]
        blockade_efficiency,
  
        [EuKeyAttribute("build_cost")]
        [Display(Name = "Construction cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        build_cost,
  
        [EuKeyAttribute("build_time")]
        [Display(Name = "Construction time")]
        [EuValueType(BonusDisplayType.Percentage)]
        build_time,
  
        [EuKeyAttribute("can_fabricate_for_vassals")]
        [Display(Name = "May fabricate claims for subjects")]
        [EuValueType(BonusDisplayType.YesNo)]
        can_fabricate_for_vassals,
  
        [EuKeyAttribute("caravan_power")]
        [Display(Name = "Caravan power")]
        [EuValueType(BonusDisplayType.Percentage)]
        caravan_power,
  
        [EuKeyAttribute("cav_to_inf_ratio")]
        [Display(Name = "Cavalry to infantry ratio")]
        [EuValueType(BonusDisplayType.Percentage)]
        cav_to_inf_ratio,
  
        [EuKeyAttribute("cavalry_cost")]
        [Display(Name = "Cavalry cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        cavalry_cost,
  
        [EuKeyAttribute("cavalry_flanking")]
        [Display(Name = "Cavalry flanking ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        cavalry_flanking,
  
        [EuKeyAttribute("cavalry_power")]
        [Display(Name = "Cavalry combat ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        cavalry_power,
  
        [EuKeyAttribute("cb_on_overseas")]
        [Display(Name = "CB on overseas")]
        [EuValueType(BonusDisplayType.YesNo)]
        cb_on_overseas,
  
        [EuKeyAttribute("cb_on_primitives")]
        [Display(Name = "CB on primitives")]
        [EuValueType(BonusDisplayType.YesNo)]
        cb_on_primitives,
  
        [EuKeyAttribute("cb_on_religious_enemies")]
        [Display(Name = "CB on religious enemies")]
        [EuValueType(BonusDisplayType.YesNo)]
        cb_on_religious_enemies,
  
        [EuKeyAttribute("church_power_modifier")]
        [Display(Name = "Church power modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        church_power_modifier,
  
        [EuKeyAttribute("claim_duration")]
        [Display(Name = "Claim duration")]
        [EuValueType(BonusDisplayType.Percentage)]
        claim_duration,
  
        [EuKeyAttribute("colonist_placement_chance")]
        [Display(Name = "Settler chance")]
        [EuValueType(BonusDisplayType.Percentage)]
        colonist_placement_chance,
  
        [EuKeyAttribute("colonists")]
        [Display(Name = "Colonists")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        colonists,
  
        [EuKeyAttribute("core_creation")]
        [Display(Name = "Core creation cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        core_creation,
  
        [EuKeyAttribute("culture_conversion_cost")]
        [Display(Name = "Culture conversion cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        culture_conversion_cost,
  
        [EuKeyAttribute("decision")]
        [Display(Name = "Decision")]
        [EuValueType(BonusDisplayType.Decision)]
        decision,
  
        [EuKeyAttribute("defensiveness")]
        [Display(Name = "Defensiveness")]
        [EuValueType(BonusDisplayType.Percentage)]
        defensiveness,
  
        [EuKeyAttribute("development_cost")]
        [Display(Name = "Development cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        development_cost,
  
        [EuKeyAttribute("devotion")]
        [Display(Name = "Yearly devotion")]
        [EuValueType(BonusDisplayType.TwoDp)]
        devotion,
  
        [EuKeyAttribute("dip_tech_cost_modifier")]
        [Display(Name = "Diplo tech cost modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        dip_tech_cost_modifier,
  
        [EuKeyAttribute("diplomatic_annexation_cost")]
        [Display(Name = "Diplomatic annexation cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        diplomatic_annexation_cost,
  
        [EuKeyAttribute("diplomatic_reputation")]
        [Display(Name = "Diplomatic reputation")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        diplomatic_reputation,
  
        [EuKeyAttribute("diplomatic_upkeep")]
        [Display(Name = "Diplomatic relations")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        diplomatic_upkeep,
  
        [EuKeyAttribute("diplomats")]
        [Display(Name = "Diplomats")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        diplomats,
  
        [EuKeyAttribute("discipline")]
        [Display(Name = "Discipline")]
        [EuValueType(BonusDisplayType.Percentage)]
        discipline,
  
        [EuKeyAttribute("drill_decay_modifier")]
        [Display(Name = "Army drill decay modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        drill_decay_modifier,
  
        [EuKeyAttribute("drill_gain_modifier")]
        [Display(Name = "Army drill gain modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        drill_gain_modifier,
  
        [EuKeyAttribute("embargo_efficiency")]
        [Display(Name = "Embargo efficiency")]
        [EuValueType(BonusDisplayType.Percentage)]
        embargo_efficiency,
  
        [EuKeyAttribute("elective_monarchy_dip_buff")]
        [Display(Name = "Elective monarchy diplomatic buff")]
        [EuValueType(BonusDisplayType.YesNo)]
        elective_monarchy_dip_buff,
  
        [EuKeyAttribute("embracement_cost")]
        [Display(Name = "Institution embracement cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        embracement_cost,
  
        [EuKeyAttribute("enemy_core_creation")]
        [Display(Name = "Enemy core creation cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        enemy_core_creation,
  
        [EuKeyAttribute("enforce_religion_cost")]
        [Display(Name = "Enforce religion cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        enforce_religion_cost,
  
        [EuKeyAttribute("expel_minorities_cost")]
        [Display(Name = "Expel minorities cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        expel_minorities_cost,
  
        [EuKeyAttribute("extra_manpower_at_religious_war")]
        [Display(Name = "Extra manpower at religious war")]
        [EuValueType(BonusDisplayType.YesNo)]
        extra_manpower_at_religious_war,
  
        [EuKeyAttribute("female_advisor_chance")]
        [Display(Name = "Female advisor chance")]
        [EuValueType(BonusDisplayType.Percentage)]
        female_advisor_chance,
  
        [EuKeyAttribute("fire_damage")]
        [Display(Name = "Fire damage")]
        [EuValueType(BonusDisplayType.Percentage)]
        fire_damage,
  
        [EuKeyAttribute("fire_damage_received")]
        [Display(Name = "Fire damage received")]
        [EuValueType(BonusDisplayType.Percentage)]
        fire_damage_received,
  
        [EuKeyAttribute("fort_maintenance_modifier")]
        [Display(Name = "Fort maintenance modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        fort_maintenance_modifier,
  
        [EuKeyAttribute("free_leader_pool")]
        [Display(Name = "Leader(s) without upkeep")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        free_leader_pool,
  
        [EuKeyAttribute("galley_cost")]
        [Display(Name = "Galley cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        galley_cost,
  
        [EuKeyAttribute("galley_power")]
        [Display(Name = "Galley combat ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        galley_power,
  
        [EuKeyAttribute("garrison_size")]
        [Display(Name = "Garrison size")]
        [EuValueType(BonusDisplayType.Percentage)]
        garrison_size,
  
        [EuKeyAttribute("global_autonomy")]
        [Display(Name = "Autonomy modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_autonomy,
  
        [EuKeyAttribute("global_colonial_growth")]
        [Display(Name = "Global settler increase")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        global_colonial_growth,
  
        [EuKeyAttribute("global_foreign_trade_power")]
        [Display(Name = "Trade power abroad")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_foreign_trade_power,
  
        [EuKeyAttribute("global_garrison_growth")]
        [Display(Name = "Global garrison growth")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_garrison_growth,
  
        [EuKeyAttribute("global_heretic_missionary_strength")]
        [Display(Name = "Missionary strength vs heretics")]
        [EuValueType(BonusDisplayType.OneDp)]
        global_heretic_missionary_strength,
  
        [EuKeyAttribute("global_institution_spread")]
        [Display(Name = "Global institution spread")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_institution_spread,
  
        [EuKeyAttribute("global_institution_growth")]
        [Display(Name = "Global institution growth")]
        [EuValueType(BonusDisplayType.OneDp)]
        global_institution_growth,
  
        [EuKeyAttribute("global_manpower")]
        [Display(Name = "National manpower")]
        [EuValueType(BonusDisplayType.Thousand)]
        global_manpower,
  
        [EuKeyAttribute("global_manpower_modifier")]
        [Display(Name = "National manpower modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_manpower_modifier,
  
        [EuKeyAttribute("global_missionary_strength")]
        [Display(Name = "Missionary strength")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_missionary_strength,
  
        [EuKeyAttribute("global_prov_trade_power_modifier")]
        [Display(Name = "Provincial trade power modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_prov_trade_power_modifier,
  
        [EuKeyAttribute("global_regiment_cost")]
        [Display(Name = "Regiment cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_regiment_cost,
  
        [EuKeyAttribute("global_regiment_recruit_speed")]
        [Display(Name = "Regimentment time")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_regiment_recruit_speed,
  
        [EuKeyAttribute("global_religious_conversion_resistance")]
        [Display(Name = "Religious conversion resistance")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_religious_conversion_resistance,
  
        [EuKeyAttribute("global_sailors")]
        [Display(Name = "National sailors")]
        [EuValueType(BonusDisplayType.Thousand)]
        global_sailors,
  
        [EuKeyAttribute("global_sailors_modifier")]
        [Display(Name = "National sailors modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_sailors_modifier,
  
        [EuKeyAttribute("global_ship_cost")]
        [Display(Name = "Ship costs")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_ship_cost,
  
        [EuKeyAttribute("global_ship_recruit_speed")]
        [Display(Name = "Shipbuilding time")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_ship_recruit_speed,
  
        [EuKeyAttribute("global_ship_repair")]
        [Display(Name = "Global ship repair")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_ship_repair,
  
        [EuKeyAttribute("global_ship_trade_power")]
        [Display(Name = "Ship trade power")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_ship_trade_power,
  
        [EuKeyAttribute("global_spy_defence")]
        [Display(Name = "Foreign spy detection")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_spy_defence,
  
        [EuKeyAttribute("global_supply_limit_modifier")]
        [Display(Name = "Global supply limit modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_supply_limit_modifier,
  
        [EuKeyAttribute("global_tariffs")]
        [Display(Name = "Tariffs")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_tariffs,
  
        [EuKeyAttribute("global_tax_modifier")]
        [Display(Name = "National tax modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_tax_modifier,
  
        [EuKeyAttribute("global_trade_goods_size_modifier")]
        [Display(Name = "Goods produced modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_trade_goods_size_modifier,
  
        [EuKeyAttribute("global_trade_power")]
        [Display(Name = "Global trade power")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_trade_power,
  
        [EuKeyAttribute("global_unrest")]
        [Display(Name = "National unrest")]
        [EuValueType(BonusDisplayType.TwoDp)]
        global_unrest,
  
        [EuKeyAttribute("governing_capacity")]
        [Display(Name = "Governing capacity")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        governing_capacity,
  
        [EuKeyAttribute("governing_capacity_modifier")]
        [Display(Name = "Governing capacity modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        governing_capacity_modifier,
  
        [EuKeyAttribute("harmonization_speed")]
        [Display(Name = "Religious harmonization speed")]
        [EuValueType(BonusDisplayType.Percentage)]
        harmonization_speed,
  
        [EuKeyAttribute("harsh_treatment_cost")]
        [Display(Name = "Harsh treatment cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        harsh_treatment_cost,
  
        [EuKeyAttribute("heavy_ship_cost")]
        [Display(Name = "Heavy ship cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        heavy_ship_cost,
  
        [EuKeyAttribute("heavy_ship_power")]
        [Display(Name = "Heavy ship combat ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        heavy_ship_power,
  
        [EuKeyAttribute("heir_chance")]
        [Display(Name = "Chance of new heir")]
        [EuValueType(BonusDisplayType.Percentage)]
        heir_chance,
  
        [EuKeyAttribute("horde_unity")]
        [Display(Name = "Yearly horde unity")]
        [EuValueType(BonusDisplayType.TwoDp)]
        horde_unity,
  
        [EuKeyAttribute("hostile_attrition")]
        [Display(Name = "Hostile attrition")]
        [EuValueType(BonusDisplayType.Percentage)]
        hostile_attrition,
  
        [EuKeyAttribute("idea_cost")]
        [Display(Name = "Idea cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        idea_cost,
  
        [EuKeyAttribute("imperial_authority")]
        [Display(Name = "Imperial authority growth modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        imperial_authority,
  
        [EuKeyAttribute("imperial_authority_value")]
        [Display(Name = "Imperial authority")]
        [EuValueType(BonusDisplayType.TwoDp)]
        imperial_authority_value,
  
        [EuKeyAttribute("imperial_mandate")]
        [EuValueType(BonusDisplayType.TwoDp)]
        [Display(Name = "Mandate")]
        imperial_mandate,
  
        [EuKeyAttribute("improve_relation_modifier")]
        [Display(Name = "Improve relations modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        improve_relation_modifier,
  
        [EuKeyAttribute("infantry_cost")]
        [Display(Name = "Infantry cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        infantry_cost,
  
        [EuKeyAttribute("infantry_power")]
        [Display(Name = "Infantry combat ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        infantry_power,
  
        [EuKeyAttribute("inflation_action_cost")]
        [Display(Name = "Reduce inflation cost reduction")]
        [EuValueType(BonusDisplayType.Percentage)]
        inflation_action_cost,
  
        [EuKeyAttribute("inflation_reduction")]
        [Display(Name = "Yearly inflation reduction")]
        [EuValueType(BonusDisplayType.TwoDp)]
        inflation_reduction,
  
        [EuKeyAttribute("inflationreduction")]
        [Display(Name = "Yearly inflation reduction")]
        [EuValueType(BonusDisplayType.ValuePerColonialNation)]
        inflationreduction,
  
        [EuKeyAttribute("innovativeness_gain")]
        [Display(Name = "Innovativeness gain")]
        [EuValueType(BonusDisplayType.Percentage)]
        innovativeness_gain,
  
        [EuKeyAttribute("institution_spread_from_true_faith")]
        [Display(Name = "Institution spread from true faith")]
        [EuValueType(BonusDisplayType.Percentage)]
        institution_spread_from_true_faith,
  
        [EuKeyAttribute("interest")]
        [Display(Name = "Interest")]
        [EuValueType(BonusDisplayType.TwoDp)]
        interest,
  
        [EuKeyAttribute("land_attrition")]
        [Display(Name = "Land attrition")]
        [EuValueType(BonusDisplayType.Percentage)]
        land_attrition,
  
        [EuKeyAttribute("land_forcelimit")]
        [Display(Name = "Land force limit")]
        [EuValueType(BonusDisplayType.OneDp)]
        land_forcelimit,
  
        [EuKeyAttribute("land_forcelimit_modifier")]
        [Display(Name = "Land force limit modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        land_forcelimit_modifier,
  
        [EuKeyAttribute("land_maintenance_modifier")]
        [Display(Name = "Land maintenance modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        land_maintenance_modifier,
  
        [EuKeyAttribute("land_morale")]
        [Display(Name = "Morale of armies")]
        [EuValueType(BonusDisplayType.Percentage)]
        land_morale,
  
        [EuKeyAttribute("leader_land_fire")]
        [Display(Name = "Land leader fire")]
        [EuValueType(BonusDisplayType.OneDp)]
        leader_land_fire,
  
        [EuKeyAttribute("leader_land_manuever")]
        [Display(Name = "Land leader maneuver")]
        [EuValueType(BonusDisplayType.OneDp)]
        leader_land_manuever,
  
        [EuKeyAttribute("leader_land_shock")]
        [Display(Name = "Land leader shock")]
        [EuValueType(BonusDisplayType.OneDp)]
        leader_land_shock,
  
        [EuKeyAttribute("leader_naval_fire")]
        [Display(Name = "Naval leader fire")]
        [EuValueType(BonusDisplayType.OneDp)]
        leader_naval_fire,
  
        [EuKeyAttribute("leader_naval_manuever")]
        [Display(Name = "Naval leader maneuver")]
        [EuValueType(BonusDisplayType.OneDp)]
        leader_naval_manuever,
  
        [EuKeyAttribute("leader_naval_shock")]
        [Display(Name = "Naval leader shock")]
        [EuValueType(BonusDisplayType.OneDp)]
        leader_naval_shock,
  
        [EuKeyAttribute("leader_siege")]
        [Display(Name = "Leader siege")]
        [EuValueType(BonusDisplayType.OneDp)]
        leader_siege,
  
        [EuKeyAttribute("legitimacy")]
        [Display(Name = "Yearly legitimacy")]
        [EuValueType(BonusDisplayType.TwoDp)]
        legitimacy,
  
        [EuKeyAttribute("liberty_desire_from_subject_development")]
        [Display(Name = "Liberty desire from subjects development")]
        [EuValueType(BonusDisplayType.Percentage)]
        liberty_desire_from_subject_development,
  
        [EuKeyAttribute("light_ship_cost")]
        [Display(Name = "Light ship cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        light_ship_cost,
  
        [EuKeyAttribute("light_ship_power")]
        [Display(Name = "Light ship combat ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        light_ship_power,
  
        [EuKeyAttribute("loot_amount")]
        [Display(Name = "Loot amount")]
        [EuValueType(BonusDisplayType.Percentage)]
        loot_amount,
  
        [EuKeyAttribute("manpower_in_true_faith_provinces")]
        [Display(Name = "Manpower in true faith provinces")]
        [EuValueType(BonusDisplayType.Percentage)]
        manpower_in_true_faith_provinces,
  
        [EuKeyAttribute("manpower_recovery_speed")]
        [Display(Name = "Manpower recovery speed")]
        [EuValueType(BonusDisplayType.Percentage)]
        manpower_recovery_speed,
  
        [EuKeyAttribute("max_absolutism")]
        [Display(Name = "Maximum absolutism")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        max_absolutism,
  
        [EuKeyAttribute("may_explore")]
        [Display(Name = "May explore")]
        [EuValueType(BonusDisplayType.YesNo)]
        may_explore,
  
        [EuKeyAttribute("may_perform_slave_raid")]
        [Display(Name = "May raid coasts")]
        [EuValueType(BonusDisplayType.YesNo)]
        may_perform_slave_raid,
  
        [EuKeyAttribute("merc_maintenance_modifier")]
        [Display(Name = "Mercenary maintenance")]
        [EuValueType(BonusDisplayType.Percentage)]
        merc_maintenance_modifier,
  
        [EuKeyAttribute("mercenary_cost")]
        [Display(Name = "Mercenary cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        mercenary_cost,
  
        [EuKeyAttribute("mercenary_discipline")]
        [Display(Name = "Mercenary discipline")]
        [EuValueType(BonusDisplayType.Percentage)]
        mercenary_discipline,
  
        [EuKeyAttribute("mercenary_manpower")]
        [Display(Name = "Mercenary manpower")]
        [EuValueType(BonusDisplayType.Percentage)]
        mercenary_manpower,
  
        [EuKeyAttribute("merchants")]
        [Display(Name = "Merchants")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        merchants,
  
        [EuKeyAttribute("meritocracy")]
        [Display(Name = "Yearly meritocracy")]
        [EuValueType(BonusDisplayType.TwoDp)]
        meritocracy,
  
        [EuKeyAttribute("mil_tech_cost_modifier")]
        [Display(Name = "Military tech cost modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        mil_tech_cost_modifier,
  
        [EuKeyAttribute("missionaries")]
        [Display(Name = "Missionaries")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        missionaries,
  
        [EuKeyAttribute("missionary_maintenance_cost")]
        [Display(Name = "Missionary maintenance cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        missionary_maintenance_cost,
  
        [EuKeyAttribute("monarch_admin_power")]
        [Display(Name = "Monarch administrative skill")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        monarch_admin_power,
  
        [EuKeyAttribute("monarch_diplomatic_power")]
        [Display(Name = "Monarch diplomatic skill")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        monarch_diplomatic_power,
  
        [EuKeyAttribute("monarch_military_power")]
        [Display(Name = "Monarch military skill")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        monarch_military_power,
  
        [EuKeyAttribute("monthly_fervor_increase")]
        [Display(Name = "Monthly fervor increase")]
        [EuValueType(BonusDisplayType.Percentage)]
        monthly_fervor_increase,
  
        [EuKeyAttribute("monthly_militarized_society")]
        [Display(Name = "Monthly militarized state")]
        [EuValueType(BonusDisplayType.TwoDp)]
        monthly_militarized_society,
  
        [EuKeyAttribute("monthly_splendor")]
        [Display(Name = "Monthly splendor")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        monthly_splendor,
  
        [EuKeyAttribute("movement_speed")]
        [Display(Name = "Movement speed")]
        [EuValueType(BonusDisplayType.Percentage)]
        movement_speed,
  
        [EuKeyAttribute("naval_attrition")]
        [Display(Name = "Naval attrition")]
        [EuValueType(BonusDisplayType.Percentage)]
        naval_attrition,
  
        [EuKeyAttribute("naval_forcelimit")]
        [Display(Name = "Naval force limit")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        naval_forcelimit,
  
        [EuKeyAttribute("naval_forcelimit_modifier")]
        [Display(Name = "Naval force limit modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        naval_forcelimit_modifier,
  
        [EuKeyAttribute("naval_maintenance_modifier")]
        [Display(Name = "Naval maintenance modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        naval_maintenance_modifier,
  
        [EuKeyAttribute("naval_morale")]
        [Display(Name = "Morale of navies")]
        [EuValueType(BonusDisplayType.Percentage)]
        naval_morale,
  
        [EuKeyAttribute("navy_tradition")]
        [Display(Name = "Yearly navy tradition")]
        [EuValueType(BonusDisplayType.TwoDp)]
        navy_tradition,
  
        [EuKeyAttribute("navy_tradition_decay")]
        [Display(Name = "Yearly navy tradition decay")]
        [EuValueType(BonusDisplayType.Percentage)]
        navy_tradition_decay,
  
        [EuKeyAttribute("num_accepted_cultures")]
        [Display(Name = "Number of accepted cultures")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        num_accepted_cultures,
  
        [EuKeyAttribute("papal_influence")]
        [Display(Name = "Yearly papal influence")]
        [EuValueType(BonusDisplayType.TwoDp)]
        papal_influence,
  
        [EuKeyAttribute("prestige")]
        [Display(Name = "Yearly prestige")]
        [EuValueType(BonusDisplayType.TwoDp)]
        prestige,
  
        [EuKeyAttribute("prestige_decay")]
        [Display(Name = "Prestige decay")]
        [EuValueType(BonusDisplayType.Percentage)]
        prestige_decay,
  
        [EuKeyAttribute("prestige_from_land")]
        [Display(Name = "Prestige from land battles")]
        [EuValueType(BonusDisplayType.Percentage)]
        prestige_from_land,
  
        [EuKeyAttribute("privateer_efficiency")]
        [Display(Name = "Privateer efficiency")]
        [EuValueType(BonusDisplayType.Percentage)]
        privateer_efficiency,
  
        [EuKeyAttribute("production_efficiency")]
        [Display(Name = "Production efficiency")]
        [EuValueType(BonusDisplayType.Percentage)]
        production_efficiency,
  
        [EuKeyAttribute("promote_culture_cost")]
        [Display(Name = "Promote culture cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        promote_culture_cost,
  
        [EuKeyAttribute("province_warscore_cost")]
        [Display(Name = "Province war score cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        province_warscore_cost,
  
        [EuKeyAttribute("range")]
        [Display(Name = "Colonial range")]
        [EuValueType(BonusDisplayType.Percentage)]
        range,
  
        [EuKeyAttribute("raze_power_gain")]
        [Display(Name = "Razing power gain")]
        [EuValueType(BonusDisplayType.Percentage)]
        raze_power_gain,
  
        [EuKeyAttribute("rebel_support_efficiency")]
        [Display(Name = "Rebel support efficiency")]
        [EuValueType(BonusDisplayType.Percentage)]
        rebel_support_efficiency,
  
        [EuKeyAttribute("recover_army_morale_speed")]
        [Display(Name = "Recover army morale speed")]
        [EuValueType(BonusDisplayType.Percentage)]
        recover_army_morale_speed,
  
        [EuKeyAttribute("recover_navy_morale_speed")]
        [Display(Name = "Recover navy morale speed")]
        [EuValueType(BonusDisplayType.Percentage)]
        recover_navy_morale_speed,
  
        [EuKeyAttribute("reduced_liberty_desire")]
        [Display(Name = "Liberty desire in subjects")]
        [EuValueType(BonusDisplayType.OneDp)]
        reduced_liberty_desire,
  
        [EuKeyAttribute("reduced_stab_impacts")]
        [Display(Name = "Reduced stability impact from diplo")]
        [EuValueType(BonusDisplayType.YesNo)]
        reduced_stab_impacts,
  
        [EuKeyAttribute("reform_progress_growth")]
        [Display(Name = "Reform progress growth")]
        [EuValueType(BonusDisplayType.Percentage)]
        reform_progress_growth,
  
        [EuKeyAttribute("reinforce_cost_modifier")]
        [Display(Name = "Reinforce cost modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        reinforce_cost_modifier,
  
        [EuKeyAttribute("reinforce_speed")]
        [Display(Name = "Reinforce speed")]
        [EuValueType(BonusDisplayType.Percentage)]
        reinforce_speed,
  
        [EuKeyAttribute("relation_with_heretics")]
        [Display(Name = "Relation with heretics")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        relation_with_heretics,
  
        [EuKeyAttribute("religious_unity")]
        [Display(Name = "Religious unity")]
        [EuValueType(BonusDisplayType.Percentage)]
        religious_unity,
  
        [EuKeyAttribute("republican_tradition")]
        [Display(Name = "Yearly republican tradition")]
        [EuValueType(BonusDisplayType.TwoDp)]
        republican_tradition,
  
        [EuKeyAttribute("sailor_maintenance_modifer")]
        [Display(Name = "Sailor maintenance modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        sailor_maintenance_modifer,
  
        [EuKeyAttribute("sailors_recovery_speed")]
        [Display(Name = "Sailor recovery speed")]
        [EuValueType(BonusDisplayType.Percentage)]
        sailors_recovery_speed,
  
        [EuKeyAttribute("same_culture_advisor_cost")]
        [Display(Name = "Cost of advisors with ruler's culture")]
        [EuValueType(BonusDisplayType.Percentage)]
        same_culture_advisor_cost,
  
        [EuKeyAttribute("ship_durability")]
        [Display(Name = "Ship durability")]
        [EuValueType(BonusDisplayType.Percentage)]
        ship_durability,
  
        [EuKeyAttribute("shock_damage")]
        [Display(Name = "Shock damage")]
        [EuValueType(BonusDisplayType.Percentage)]
        shock_damage,
  
        [EuKeyAttribute("shock_damage_received")]
        [Display(Name = "Shock damage received")]
        [EuValueType(BonusDisplayType.Percentage)]
        shock_damage_received,
  
        [EuKeyAttribute("siege_ability")]
        [Display(Name = "Siege ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        siege_ability,
  
        [EuKeyAttribute("siege_blockade_progress")]
        [Display(Name = "Siege blockade progress")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        siege_blockade_progress,
  
        [EuKeyAttribute("special_unit_forcelimit")]
        [Display(Name = "Special unit force limit")]
        [EuValueType(BonusDisplayType.Percentage)]
        special_unit_forcelimit,
  
        [EuKeyAttribute("spy_offence")]
        [Display(Name = "Spy network construction")]
        [EuValueType(BonusDisplayType.Percentage)]
        spy_offence,
  
        [EuKeyAttribute("stability_cost_modifier")]
        [Display(Name = "Stability cost modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        stability_cost_modifier,
  
        [EuKeyAttribute("state_maintenance_modifier")]
        [Display(Name = "State maintenance modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        state_maintenance_modifier,
  
        [EuKeyAttribute("technology_cost")]
        [Display(Name = "Technology cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        technology_cost,
  
        [EuKeyAttribute("tolerance_heathen")]
        [Display(Name = "Tolerance of heathens")]
        [EuValueType(BonusDisplayType.TwoDp)]
        tolerance_heathen,
  
        [EuKeyAttribute("tolerance_heretic")]
        [Display(Name = "Tolerance of heretics")]
        [EuValueType(BonusDisplayType.TwoDp)]
        tolerance_heretic,
  
        [EuKeyAttribute("tolerance_own")]
        [Display(Name = "Tolerance of true faith")]
        [EuValueType(BonusDisplayType.TwoDp)]
        tolerance_own,
  
        [EuKeyAttribute("trade_company_governing_cost")]
        [Display(Name = "Trade company governing cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        trade_company_governing_cost,
  
        [EuKeyAttribute("trade_efficiency")]
        [Display(Name = "Trade efficiency")]
        [EuValueType(BonusDisplayType.Percentage)]
        trade_efficiency,
  
        [EuKeyAttribute("trade_range_modifier")]
        [Display(Name = "Trade range modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        trade_range_modifier,
  
        [EuKeyAttribute("trade_steering")]
        [Display(Name = "Trade steering")]
        [EuValueType(BonusDisplayType.Percentage)]
        trade_steering,
  
        [EuKeyAttribute("transport_cost")]
        [Display(Name = "Transport cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        transport_cost,
  
        [EuKeyAttribute("transport_power")]
        [Display(Name = "Transport combat ability")]
        [EuValueType(BonusDisplayType.Percentage)]
        transport_power,
  
        [EuKeyAttribute("unjustified_demands")]
        [Display(Name = "Unjustified demands")]
        [EuValueType(BonusDisplayType.Percentage)]
        unjustified_demands,
  
        [EuKeyAttribute("war_exhaustion")]
        [Display(Name = "War exhaustion")]
        [EuValueType(BonusDisplayType.TwoDp)]
        war_exhaustion,
  
        [EuKeyAttribute("war_exhaustion_cost")]
        [Display(Name = "Cost of reducing war exhuastion")]
        [EuValueType(BonusDisplayType.Percentage)]
        war_exhaustion_cost,
  
        [EuKeyAttribute("war_taxes_cost_modifier")]
        [Display(Name = "War taxes cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        war_taxes_cost_modifier,
  
        [EuKeyAttribute("warscore_cost_vs_other_religion")]
        [Display(Name = "War score cost vs other religions")]
        [EuValueType(BonusDisplayType.Percentage)]
        warscore_cost_vs_other_religion,
  
        [EuKeyAttribute("yearly_absolutism")]
        [Display(Name = "Yearly absolutism")]
        [EuValueType(BonusDisplayType.TwoDp)]
        yearly_absolutism,
  
        [EuKeyAttribute("yearly_army_professionalism")]
        [Display(Name = "Yearly army professionalism")]
        [EuValueType(BonusDisplayType.Percentage)]
        yearly_army_professionalism,
  
        [EuKeyAttribute("yearly_corruption")]
        [Display(Name = "Yearly corruption")]
        [EuValueType(BonusDisplayType.TwoDp)]
        yearly_corruption,
  
        [EuKeyAttribute("yearly_harmony")]
        [Display(Name = "Yearly harmony increase")]
        [EuValueType(BonusDisplayType.Percentage)]
        yearly_harmony,
  
        [EuKeyAttribute("yearly_patriarch_authority")]
        [Display(Name = "Yearly patriarch authority")]
        [EuValueType(BonusDisplayType.Percentage)]
        yearly_patriarch_authority,
  
        [EuKeyAttribute("years_of_nationalism")]
        [Display(Name = "Years of separatism")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        years_of_nationalism,
  
        [EuKeyAttribute("morale_bonus_5_cultures")]
        [Display(Name = "Morale bonus from 5 cultures")]
        [EuValueType(BonusDisplayType.YesNo)]
        morale_bonus_5_cultures,
  
        [EuKeyAttribute("build_cost_in_colo_nation")]
        [Display(Name = "Build cost in colonial nation")]
        [EuValueType(BonusDisplayType.Percentage)]
        build_cost_in_colo_nation,
  
        [EuKeyAttribute("no_prof_loss_for_mercs")]
        [Display(Name = "No professionalism loss from mercs")]
        [EuValueType(BonusDisplayType.YesNo)]
        no_prof_loss_for_mercs,
  
        [EuKeyAttribute("capture_ship_chance")]
        [Display(Name = "Ship capture chance")]
        [EuValueType(BonusDisplayType.Percentage)]
        capture_ship_chance,
  
        [EuKeyAttribute("native_uprising_chance")]
        [Display(Name = "Native uprising chance")]
        [EuValueType(BonusDisplayType.Percentage)]
        native_uprising_chance,
  
        [EuKeyAttribute("monthly_piety")]
        [Display(Name = "Monthly piety")]
        [EuValueType(BonusDisplayType.TwoDp)]
        monthly_piety,
  
        [EuKeyAttribute("monthly_karma")]
        [Display(Name = "Montly karma")]
        [EuValueType(BonusDisplayType.TwoDp)]
        monthly_karma,
  
        [EuKeyAttribute("prestige_per_development_from_conversion")]
        [Display(Name = "Prestige per development from conversion")]
        [EuValueType(BonusDisplayType.TwoDp)]
        prestige_per_development_from_conversion,
  
        [EuKeyAttribute("native_assimilation")]
        [Display(Name = "Native assimilation")]
        [EuValueType(BonusDisplayType.Percentage)]
        native_assimilation,
  
        [EuKeyAttribute("rival_border_fort_maintenance")]
        [Display(Name = "Fort maintenance on border with rival")]
        [EuValueType(BonusDisplayType.Percentage)]
        rival_border_fort_maintenance,
  
        [EuKeyAttribute("vassal_income")]
        [Display(Name = "Income from vassals")]
        [EuValueType(BonusDisplayType.Percentage)]
        vassal_income,
  
        [EuKeyAttribute("sea_repair")]
        [Display(Name = "Sea repair")]
        [EuValueType(BonusDisplayType.YesNo)]
        sea_repair,
  
        [EuKeyAttribute("may_perform_slave_raid_on_same_religion")]
        [Display(Name = "May raid coasts (same religion)")]
        [EuValueType(BonusDisplayType.YesNo)]
        may_perform_slave_raid_on_same_religion,
  
        [EuKeyAttribute("core_decay_on_your_own")]
        [Display(Name = "Core decay on your own")]
        [EuValueType(BonusDisplayType.YesNo)]
        core_decay_on_your_own,

        [EuKeyAttribute("global_own_trade_power")]
        [Display(Name = "Global own trade power")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_own_trade_power,
        [EuKeyAttribute("elective_monarchy_add_buff")]
        [Display(Name = "Elective Monarchy Add Buff")]
        [EuValueType(BonusDisplayType.YesNo)]
        elective_monarchy_add_buff,
        [EuKeyAttribute("Inflationreduction")]
        [Display(Name = "Inflationreduction")]
        [EuValueType(BonusDisplayType.ValuePerColonialNation)]
        Inflationreduction,
        [EuKeyAttribute("add_stand")]
        [Display(Name = "Add estate")]
        [EuValueType(BonusDisplayType.Estate)]
        add_stand,
        [EuKeyAttribute("add_age_forcelimit_land")]
        [Display(Name = "Land force limit per age")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        add_age_forcelimit_land,
        [EuKeyAttribute("add_age_fire_damage_received")]
        [Display(Name = "Fire damage received per age")]
        [EuValueType(BonusDisplayType.Percentage)]
        add_age_fire_damage_received,
        [EuKeyAttribute("vassal_forcelimit_bonus")]
        [Display(Name = "Vassal force limit bonus")]
        [EuValueType(BonusDisplayType.Percentage)]
        vassal_forcelimit_bonus,
        [EuKeyAttribute("idea_claim_colonies")]
        [Display(Name = "Claim colonies")]
        [EuValueType(BonusDisplayType.YesNo)]
        idea_claim_colonies,
        [EuKeyAttribute("ship_power_propagation")]
        [Display(Name = "Ship Power Propagation")]
        [EuValueType(BonusDisplayType.Percentage)]
        ship_power_propagation,
        [EuKeyAttribute("may_recruit_female_generals")]
        [Display(Name = "May recruit female generals")]
        [EuValueType(BonusDisplayType.YesNo)]
        may_recruit_female_generals,
        [EuKeyAttribute("own_coast_naval_combat_bonus")]
        [Display(Name = "Naval combat bonus off own coast")]
        [EuValueType(BonusDisplayType.YesNo)]
        own_coast_naval_combat_bonus,
        [EuKeyAttribute("placed_merchant_power")]
        [Display(Name = "Merchat trade power")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        placed_merchant_power,
        [EuKeyAttribute("envoy_travel_time")]
        [Display(Name = "Envoy travel time")]
        [EuValueType(BonusDisplayType.Percentage)]
        envoy_travel_time,
        [EuKeyAttribute("army_tradition_from_battle")]
        [Display(Name = "Army tradition from battle")]
        [EuValueType(BonusDisplayType.Percentage)]
        army_tradition_from_battle,
        [EuKeyAttribute("possible_condottieri")]
        [Display(Name = "Possible condottieri")]
        [EuValueType(BonusDisplayType.Percentage)]
        possible_condottieri,
        [EuKeyAttribute("fabricate_claims_cost")]
        [Display(Name = "Fabricate claims cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        fabricate_claims_cost,
        [EuKeyAttribute("free_policy")]
        [Display(Name = "Free policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        free_policy,
        [EuKeyAttribute("power_projection_from_insults")]
        [Display(Name = "Power projection from insults")]
        [EuValueType(BonusDisplayType.Percentage)]
        power_projection_from_insults,
        [EuKeyAttribute("all_power_cost")]
        [Display(Name = "All power cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        all_power_cost,
        [EuKeyAttribute("free_adm_policy")]
        [Display(Name = "Free admin policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        free_adm_policy,
        [EuKeyAttribute("free_dip_policy")]
        [Display(Name = "Free diplomatic policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        free_dip_policy,
        [EuKeyAttribute("free_mil_policy")]
        [Display(Name = "Free military policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        free_mil_policy,
        [EuKeyAttribute("cavalry_shock")]
        [Display(Name = "Cavalry shock")]
        [EuValueType(BonusDisplayType.TwoDp)]
        cavalry_shock,
        [EuKeyAttribute("candidate_random_bonus")]
        [Display(Name = "Candidate random bonus")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        candidate_random_bonus,
        [EuKeyAttribute("election_cycle")]
        [Display(Name = "Election cycle")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        election_cycle,
        [EuKeyAttribute("naval_tradition_from_trade")]
        [Display(Name = "Naval tradition from trade")]
        [EuValueType(BonusDisplayType.Percentage)]
        naval_tradition_from_trade,
        [EuKeyAttribute("infantry_shock")]
        [Display(Name = "Infantry shock")]
        [EuValueType(BonusDisplayType.TwoDp)]
        infantry_shock,
        [EuKeyAttribute("mages_loyalty_modifier")]
        [Display(Name = "Mages loyalty modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        mages_loyalty_modifier,
        [EuKeyAttribute("administrative_efficiency")]
        [Display(Name = "Administrative efficiency")]
        [EuValueType(BonusDisplayType.Percentage)]
        administrative_efficiency,
        [EuKeyAttribute("nobles_loyalty_modifier")]
        [Display(Name = "Nobles loyalty modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        nobles_loyalty_modifier,
        [EuKeyAttribute("center_of_trade_upgrade_cost")]
        [Display(Name = "Center of trade upgrade cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        center_of_trade_upgrade_cost,
        [EuKeyAttribute("adventurers_loyalty_modifier")]
        [Display(Name = "Adventurers loyalty modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        adventurers_loyalty_modifier,
        [EuKeyAttribute("global_naval_barrage_cost")]
        [Display(Name = "Global naval barrage cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_naval_barrage_cost,
        [EuKeyAttribute("no_religion_penalty")]
        [Display(Name = "No religion penalty")]
        [EuValueType(BonusDisplayType.YesNo)]
        no_religion_penalty,
        [EuKeyAttribute("artillery_fire")]
        [Display(Name = "Artillery fire")]
        [EuValueType(BonusDisplayType.TwoDp)]
        artillery_fire,
        [EuKeyAttribute("adm_advisor_cost")]
        [Display(Name = "Admin advisor cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        adm_advisor_cost,
        [EuKeyAttribute("mil_advisor_cost")]
        [Display(Name = "Military advisor cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        mil_advisor_cost,
        [EuKeyAttribute("dip_advisor_cost")]
        [Display(Name = "Diplomatic advisor cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        dip_advisor_cost,
        [EuKeyAttribute("reelection_cost")]
        [Display(Name = "Re-election cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        reelection_cost,
        [EuKeyAttribute("possible_policy")]
        [Display(Name = "Possible policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        possible_policy,
        [EuKeyAttribute("reserves_organisation")]
        [Display(Name = "Reserves organisation")]
        [EuValueType(BonusDisplayType.Percentage)]
        reserves_organisation,
        [EuKeyAttribute("infantry_fire")]
        [Display(Name = "Infantry fire")]
        [EuValueType(BonusDisplayType.TwoDp)]
        infantry_fire,
        [EuKeyAttribute("reduced_liberty_desire_on_same_continent")]
        [Display(Name = "Reduced liberty desire on same continent")]
        [EuValueType(BonusDisplayType.Percentage)]
        reduced_liberty_desire_on_same_continent,
        [EuKeyAttribute("discovered_relations_impact")]
        [Display(Name = "Discovered relations impact")]
        [EuValueType(BonusDisplayType.Percentage)]
        discovered_relations_impact,
        [EuKeyAttribute("supply_limit_modifier")]
        [Display(Name = "Supply limit modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        supply_limit_modifier,
        [EuKeyAttribute("mercantilism_cost")]
        [Display(Name = "Mercantilism cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        mercantilism_cost,
        [EuKeyAttribute("expand_administration_cost")]
        [Display(Name = "Expand administration cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        expand_administration_cost,
        [EuKeyAttribute("available_province_loot")]
        [Display(Name = "Available province loot")]
        [EuValueType(BonusDisplayType.Percentage)]
        available_province_loot,
        [EuKeyAttribute("stability_cost_to_declare_war")]
        [Display(Name = "Stability cost to declare war")]
        [EuValueType(BonusDisplayType.Percentage)]
        stability_cost_to_declare_war,
        [EuKeyAttribute("max_revolutionary_zeal")]
        [Display(Name = "Max revolutionary zeal")]
        [EuValueType(BonusDisplayType.TwoDp)]
        max_revolutionary_zeal,
        [EuKeyAttribute("leader_cost")]
        [Display(Name = "Leader cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        leader_cost,
        [EuKeyAttribute("movement_speed_onto_off_boat_modifier")]
        [Display(Name = "Movement speed on/off boat modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        movement_speed_onto_off_boat_modifier,
        [EuKeyAttribute("global_naval_engagement_modifier")]
        [Display(Name = "Global naval engagement modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        global_naval_engagement_modifier,
        [EuKeyAttribute("may_establish_frontier")]
        [Display(Name = "May establish frontier")]
        [EuValueType(BonusDisplayType.YesNo)]
        may_establish_frontier,
        [EuKeyAttribute("cavalry_fire")]
        [Display(Name = "Cavalry fire")]
        [EuValueType(BonusDisplayType.TwoDp)]
        cavalry_fire,
        [EuKeyAttribute("church_loyalty_modifier")]
        [Display(Name = "Church loyalty modifier")]
        [EuValueType(BonusDisplayType.TwoDp)]
        church_loyalty_modifier,
        [EuKeyAttribute("possible_adm_policy")]
        [Display(Name = "Possible admin policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        possible_adm_policy,
        [EuKeyAttribute("possible_dip_policy")]
        [Display(Name = "Possible diplomatic policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        possible_dip_policy,
        [EuKeyAttribute("possible_mil_policy")]
        [Display(Name = "Possible military policy")]
        [EuValueType(BonusDisplayType.ZeroDp)]
        possible_mil_policy,
        [EuKeyAttribute("auto_explore_adjacent_to_colony")]
        [Display(Name = "Auto-explore adjacent to colony")]
        [EuValueType(BonusDisplayType.YesNo)]
        auto_explore_adjacent_to_colony,
        [EuKeyAttribute("prestige_from_naval")]
        [Display(Name = "Prestige from naval")]
        [EuValueType(BonusDisplayType.Percentage)]
        prestige_from_naval,
        [EuKeyAttribute("general_cost")]
        [Display(Name = "General cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        general_cost,
        [EuKeyAttribute("nobles_influence_modifier")]
        [Display(Name = "Nobles influence modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        nobles_influence_modifier,
        [EuKeyAttribute("rival_change_cost")]
        [Display(Name = "Rival change cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        rival_change_cost,
        [EuKeyAttribute("burghers_loyalty_modifier")]
        [Display(Name = "Burghers loyalty modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        burghers_loyalty_modifier,
        [EuKeyAttribute("artificers_loyalty_modifier")]
        [Display(Name = "Artificers loyalty modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        artificers_loyalty_modifier,
        [EuKeyAttribute("local_missionary_strength")]
        [Display(Name = "Local missionary strength")]
        [EuValueType(BonusDisplayType.Percentage)]
        local_missionary_strength,
        [EuKeyAttribute("artificers_influence_modifier")]
        [Display(Name = "Artificers influence modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        artificers_influence_modifier,
        [EuKeyAttribute("trade_goods_size")]
        [Display(Name = "Trade goods size")]
        [EuValueType(BonusDisplayType.Percentage)]
        trade_goods_size,
        [EuKeyAttribute("local_defensiveness")]
        [Display(Name = "Local defensiveness")]
        [EuValueType(BonusDisplayType.Percentage)]
        local_defensiveness,
        [EuKeyAttribute("local_manpower_modifier")]
        [Display(Name = "Local manpower modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        local_manpower_modifier,
        [EuKeyAttribute("local_monthly_devastation")]
        [Display(Name = "Local monthly devastation")]
        [EuValueType(BonusDisplayType.TwoDp)]
        local_monthly_devastation,
        [EuKeyAttribute("garrison_growth")]
        [Display(Name = "Garrison growth")]
        [EuValueType(BonusDisplayType.Percentage)]
        garrison_growth,
        [EuKeyAttribute("trade_value_modifier")]
        [Display(Name = "Trade value modifier")]
        [EuValueType(BonusDisplayType.Percentage)]
        trade_value_modifier,
        [EuKeyAttribute("local_development_cost")]
        [Display(Name = "Local development cost")]
        [EuValueType(BonusDisplayType.Percentage)]
        local_development_cost,
        [EuKeyAttribute("local_hostile_attrition")]
        [Display(Name = "Local hostile attrition")]
        [EuValueType(BonusDisplayType.Percentage)]
        local_hostile_attrition,
    }
}
