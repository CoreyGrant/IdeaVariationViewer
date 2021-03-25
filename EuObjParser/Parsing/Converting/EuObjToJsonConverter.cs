using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EuObjParser.Parsing.Converting
{
	class EuObjToJsonConverter
	{
		public string ConvertToJsonString(string euObj, bool noSpaces = true)
		{
			// Remove empty lines and comments
			euObj = string.Join(
				'\n',
				euObj.Split('\n')
					.Select(x => x.Trim())
					.Select(x => x.Contains('#')
						? x.Substring(0, x.IndexOf('#')).Trim()
						: x)
					.Where(x => !string.IsNullOrWhiteSpace(x)));

			// remove any nasty unicode crap
			euObj = Regex.Replace(euObj, @"[^\u0000-\u007F]+", string.Empty, RegexOptions.Multiline);

			// Replace equals with semi-colon
			euObj = euObj.Replace('=', ':');
			// replace obnoxious exceptions
			euObj = euObj
				.Replace("\"Common Sense\"", "common_sense");
			// Handle colors
			euObj = new Regex("\\{ +([0-9]{1,3}) +([0-9]{1,3}) +([0-9]{1,3}) +\\}", RegexOptions.Multiline).Replace(euObj, (Match match) =>
			{
				var groups = match.Groups;
				return $"\"{groups[1]}/{groups[2]}/{groups[3]}\"";
			});
			// Wrap everything of interest in quotes
			euObj = new Regex("\"?([\\w\\.(\\- ?')\\/]{2,})\"?", RegexOptions.Multiline).Replace(euObj, (Match match) =>
			{
				var val = match.Groups[1].Value;
				return $"\"{val.Trim()}\"";
			});

			if (noSpaces)
			{
				euObj = euObj.Replace(" ", "");
			}
			// add commas to every newline
			euObj = new Regex("[\\{]?[\n]{1}", RegexOptions.Multiline).Replace(euObj, (Match match) =>
			{
				var matchVal = match.Value;
				return matchVal.Contains("{") ? matchVal : ",";
			});

			euObj = euObj.Replace("\n", "");
			euObj = euObj.Replace(",}", "}");
			var nameIds = new Dictionary<string, int>();
			euObj = new Regex("(\"religion_group)|(\"religion)|(\"modifier)|(\"has_idea_group)|(\"full_idea_group)|(\"has_active_policy)").Replace(euObj, (Match match) =>
			{
				nameIds[match.Value] = nameIds.ContainsKey(match.Value) ? (nameIds[match.Value] + 1) : 0;
				return match.Value + "__" + nameIds[match.Value];
			});
			return euObj;
		}
	}
}
