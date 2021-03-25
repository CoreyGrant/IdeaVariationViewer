using EuObjParser.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace EuObjParser.Models
{
	class Bonus : IEquatable<Bonus>
	{
		public string DisplayValue => Helpers.DisplayValue(Type, Value);
		public string Url => Helpers.GetImageUrl(Type);
		public string TypeName => Helpers.GetName(Type);
		[EuObjPropName]
		public Enums.Bonus Type { get; private set; }
		[EuObjPropValue]
		public string Value { get; private set; }

		public Bonus Copy()
		{
			return new Bonus
			{
				Type = Type,
				Value = Value,
			};
		}

		public bool Equals([AllowNull] Bonus other)
		{
			return other.Type == Type;
		}
	}
}
