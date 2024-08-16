using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EchoesofBlue.scripts.serialization;

public class CoordinateConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(Coordinate);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader is not { TokenType: JsonToken.StartArray })
			throw new JsonSerializationException($"Invalid token type {existingValue} {reader.ValueType}");
		Coordinate instance = new();
		var array = JArray.Load(reader);
		List<float> list = [];
		foreach (var item in array) list.Add(item.ToObject<float>(serializer));
		instance.X = list[0];
		instance.Y = list[1];
		return instance;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		if (value is Coordinate instance) serializer.Serialize(writer, (List<float>) [instance.X, instance.Y]);
		else throw new JsonSerializationException("Value cannot be null");
	}
}