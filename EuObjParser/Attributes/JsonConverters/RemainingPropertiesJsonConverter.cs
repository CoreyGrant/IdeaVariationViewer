using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EuObjParser.Attributes.JsonConverters
{
	class RemainingPropertiesJsonConverter<T> : JsonConverter<IReadOnlyCollection<T>>
	{
		public override IReadOnlyCollection<T> ReadJson(JsonReader reader, Type objectType, [AllowNull] IReadOnlyCollection<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			//reader.
			return null;
		}

		public override void WriteJson(JsonWriter writer, [AllowNull] IReadOnlyCollection<T> value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
