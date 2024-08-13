using System;
using System.Reflection;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class StringConverter<T> : JsonConverter where T : class
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(T);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType != JsonToken.String) throw new JsonSerializationException("Invalid token type");
		var instance = Activator.CreateInstance<T>();
		var property = typeof(T).GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
		if (property != null) property.SetValue(instance, (string)reader.Value);
		return instance;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		if (value is T instance)
		{
			var property = typeof(T).GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
			if (property != null) serializer.Serialize(writer, (string) property.GetValue(instance));
			else throw new JsonSerializationException("Value cannot be null");
		}
		else throw new JsonSerializationException("Value cannot be null");
	}
}