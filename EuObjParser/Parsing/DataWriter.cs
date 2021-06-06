using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuObjParser.Parsing
{
	class DataWriter
	{
		public DataWriter()
		{
		}

		private DefaultContractResolver _contractResolver = new DefaultContractResolver
		{
			NamingStrategy = new CamelCaseNamingStrategy()
		};

		public static string ProjectBaseFolder = AppDomain.CurrentDomain.BaseDirectory.Split("\\bin")[0];

		public void WriteJObject(string filename, JToken obj, string folder = null)
		{
			if (folder != null)
			{
				var folderPath = Path.Combine(ProjectBaseFolder, folder);
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}
				filename = Path.Combine(folder, filename);
			}
			var objString = obj.ToString(Formatting.None);
			var windows1252 = Encoding.GetEncoding(1252);
			var objStringBytes = windows1252.GetBytes(objString);
			var utf8Btyes = Encoding.Convert(windows1252, Encoding.UTF8, objStringBytes);
			var utf8String = Encoding.UTF8.GetString(utf8Btyes);
			File.WriteAllText(Path.Combine(ProjectBaseFolder, filename + ".json"), utf8String);
		}

		public void Write(string filename, object obj, string folder = null)
		{
			if(folder != null)
			{
				var folderPath = Path.Combine(ProjectBaseFolder, folder);
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}
				filename = Path.Combine(folder, filename);
			}
			var objString = JsonConvert.SerializeObject(
					obj,
					Formatting.None,
					new JsonSerializerSettings
					{
						ContractResolver = _contractResolver,
						NullValueHandling = NullValueHandling.Ignore,
						
					});
			var windows1252 = Encoding.GetEncoding(1252);
			var objStringBytes = windows1252.GetBytes(objString);
			var utf8Btyes = Encoding.Convert(windows1252, Encoding.UTF8, objStringBytes);
			var utf8String = Encoding.UTF8.GetString(utf8Btyes);
			File.WriteAllText(@$"{ProjectBaseFolder}\{filename}.json", utf8String);
		}
	}
}
