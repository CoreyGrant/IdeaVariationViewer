using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Json
{
	class Country
	{
		[JsonProperty("_filename")]
		public string Filename { get; set; }
		[JsonProperty("color")]
		public IReadOnlyCollection<string> Color { get; set; }
	}
}
