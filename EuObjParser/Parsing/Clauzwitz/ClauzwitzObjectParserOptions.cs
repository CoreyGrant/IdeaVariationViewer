using System;
using System.Linq;
using System.Text;

namespace EuObjParser.Parsing.Clauzwitz
{
	class ClauzwitzObjectParserOptions
	{
		public Func<string, string> PropertyNameMapping { get; set; } = (propName) =>
		{
			while (true)
			{
				var indexOfUpperCase = propName
					.Select((x, i) => new { u = char.IsUpper(x), i, c = x })
					.FirstOrDefault(x => x.u) ?? new { u = false, i = -1, c = (char)0 };
				if (indexOfUpperCase.i == -1)
				{
					break;
				}
				propName = propName.Remove(indexOfUpperCase.i).Insert(indexOfUpperCase.i, "_" + indexOfUpperCase.c.ToString());

			}
			return propName.ToLower();
		};
	}

}
