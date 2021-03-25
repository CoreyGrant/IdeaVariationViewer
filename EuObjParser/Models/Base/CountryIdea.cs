using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EuObjParser.Models.Base
{
	class CountryIdea
	{
		public string CountryTag => Type.Replace("_ideas", "");
		[EuObjPropName]
		[JsonIgnore]
		public string Type { get; private set; }
		[EuPropNameRemaining("free")]
		public List<Idea> Ideas { get; private set; }
		[EuPropName("trigger")]
		public CountryIdeaTrigger Trigger { get; private set; }

	}

	class CountryIdeaTrigger
	{
		[EuPropName("culture_group")]
		public string CultureGroup { get; set; }
		[EuPropName("primary_culture")]
		public string PrimaryCulture { get; set; }
		[EuObjCollapse("OR")]
		[EuObjList("culture_group")]
		public List<string> AnyCultureGroup { get; set; }
		[EuObjCollapse("OR")]
		[EuObjList("primary_culture")]
		public List<string> AnyPrimaryCulture { get; set; }
		[EuObjList("tag")]
		public List<string> Tag { get; set; }
		[EuObjCollapse("OR")]
		[EuObjList("tag")]
		public List<string> AnyTag { get; set; }
	}
}
