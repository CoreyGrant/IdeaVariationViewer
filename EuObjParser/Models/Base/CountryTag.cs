using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EuObjParser.Models.Base
{
	class CountryTag
	{
		[EuObjPropName]
		public string Tag { get; set; }
		[JsonIgnore]
		[EuObjPropValue]
		public string CountryLocation { get; set; }
		public string Country => CountryLocation.Replace("countries/", "").Replace(".txt", "");
	}
}
