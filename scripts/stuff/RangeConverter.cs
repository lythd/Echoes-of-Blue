using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EchoesofBlue.scripts.serialization;

public class RangeConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(Range);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType != JsonToken.Integer)
		{
			if (reader.TokenType != JsonToken.StartArray) throw new JsonSerializationException($"Invalid token type {existingValue} {reader.ValueType}");
			Range instance = new();
			var array = JArray.Load(reader);
			List<long> list = [];
			foreach (var item in array) list.Add(item.ToObject<long>(serializer));
			instance.RangeValues = list;
			instance.IsRange = true;
			return instance;
		}
		else
		{
			if (reader.Value == null) throw new JsonSerializationException($"Invalid token type {existingValue} {reader.ValueType}");
			Range instance = new()
			{
				SingleValue = (long)reader.Value,
				IsRange = false
			};
			return instance;
		}
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		if (value is Range instance)
		{
			if (instance.IsRange) serializer.Serialize(writer, instance.RangeValues);
			else serializer.Serialize(writer, instance.SingleValue);
		}
		else throw new JsonSerializationException("Value cannot be null");
	}
}