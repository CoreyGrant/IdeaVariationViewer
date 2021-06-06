using EuObjParser.Parsing.Clauzwitz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuObjParser.Models.Clauzwitz.religions
{
	class ChurchAspect
	{
		[PropertyName]
		public string Name { get; set; }
		public bool IsBlessing { get; set; }
		public IReadOnlyDictionary<string, string> Modifier { get; set; }
		public int Cost { get; set; }
	}
}
