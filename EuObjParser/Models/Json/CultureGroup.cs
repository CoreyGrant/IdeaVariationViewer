using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Json
{
	class CultureGroup
	{
		public string Name { get; set; }
		public List<Culture> Cultures { get; set; }
	}

	class Culture
	{
		public string Name { get; set; }
		public string Primary { get; set; }
	}
}
