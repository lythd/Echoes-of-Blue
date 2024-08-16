using System;
using System.Numerics;
using System.Reflection;
using Newtonsoft.Json;
using static System.Numerics.BigInteger;

namespace EchoesofBlue.scripts.serialization;

public class BigIntegerConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(BigInteger);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType != JsonToken.String) throw new JsonSerializationException($"Invalid token type {existingValue} {reader.ValueType}");
		if (reader.Value == null) return new();
		return TryParse((string)reader.Value, out var bigInteger) ? bigInteger : new();
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		if (value is BigInteger instance) serializer.Serialize(writer, instance.ToString());
		else throw new JsonSerializationException("Value cannot be null");
	}
}