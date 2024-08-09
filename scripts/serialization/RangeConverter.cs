using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class RangeConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(Range);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Integer)
		{
			Range instance = new();
			instance.SingleValue = (long)reader.Value;
			instance.IsRange = false;
			return instance;
		}
		else if (reader.TokenType == JsonToken.StartArray)
		{
			Range instance = new();
			JArray array = JArray.Load(reader);
			List<long> list = new List<long>();
			foreach (JToken item in array) list.Add(item.ToObject<long>(serializer));
			instance.RangeValues = list;
			instance.IsRange = true;
			return instance;
		}
		throw new JsonSerializationException("Invalid token type");
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		Range instance = value as Range;
		if (instance != null)
		{
			if (instance.IsRange) serializer.Serialize(writer, instance.RangeValues);
			else serializer.Serialize(writer, (long) instance.SingleValue);
		}
		else throw new JsonSerializationException("Value cannot be null");
	}
}
