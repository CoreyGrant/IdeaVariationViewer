using EuObjParser.Models.Clauzwitz.countries;
using EuObjParser.Models.Clauzwitz.greatProjects;
using EuObjParser.Models.Clauzwitz.ideas;
using EuObjParser.Models.Clauzwitz.policies;
using EuObjParser.Models.Clauzwitz.religions;
using EuObjParser.Models.Clauzwitz.shared;
using EuObjParser.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Config
{
	class FileMappingsResult
	{
		public string ModName { get; set; }
		public long Id { get; set; }
		public FileMappingsSections Sections { get; set; }
		public List<IdeaGroup> IdeaGroups { get; set; }
		public List<IdeaGroup> CountryIdeas { get; set; }
		public List<Policy> Policies { get; set; }
		public IReadOnlyDictionary<string, string> CountryTags { get; set; }
		public List<Country> Countries { get; set; }
		public List<CountryHistory> CountryHistories { get; set; }
		public List<CultureGroup> CultureGroups { get; set; }
		public List<ReligionGroup> ReligionGroups { get; set; }
		public List<ChurchAspect> ChurchAspects { get; set; }
		public List<GreatProject> GreatProjects { get; set; }
		public List<ProvinceHistory> ProvinceHistories { get; set; }
		public Dictionary<string, string> ProvinceNames { get; set; }
		public List<string> Bonuses { get; set; }
	}

	class BaseGameFileMappingsResult : FileMappingsResult
	{
		public List<string> ProvinceHistoriesFiles { get; set; }
		public List<string> IdeasFiles { get; set; }
		public List<string> PoliciesFiles { get; set; }
		public List<string> CountryTagsFiles { get; set; }
		public List<string> CountriesFiles { get; set; }
		public List<string> CountryHistoriesFiles { get; set; }
		public List<string> CultureGroupsFiles { get; set; }
		public List<string> ReligionGroupsFiles { get; set; }
		public List<string> ChurchAspectFiles { get; set; }
		public List<string> GreatProjectFiles { get; set; }
		public string ProvinceNamesFile => "prov_names_l_english.yml";
	}


	class CombinedFileMappingResult
	{
		public string ModName { get; set; }
		public long Id { get; set; }
		public List<IdeaGroup> IdeaGroups { get; set; }
		public List<Models.Output.Policies.Policy> Policies { get; set; }
		public List<Models.Output.Countries.Country> Countries { get; set; }
		public List<Models.Output.Religions.ReligionGroup> ReligionGroups { get; set; }
		public List<string> Bonuses { get; set; }
	}

}
