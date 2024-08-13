using System;
using System.Reflection;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class GameConverter<T> : JsonConverter where T : class
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(T);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.PropertyName)
		{
			var t = typeof(T);
			var methodInfo = t.GetMethod("Get", BindingFlags.Static | BindingFlags.Public);
			if (methodInfo != null) return methodInfo.Invoke(null, [(string)reader.Value]);
			else throw new MissingMethodException($"The method 'Get' does not exist on type '{t.FullName}'."); //this should never run but just incase
		}
		else throw new JsonSerializationException("Invalid token type");
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		if (value is GameEntity instance) serializer.Serialize(writer, instance.Id);
		else throw new JsonSerializationException("Value cannot be null");
	}
}