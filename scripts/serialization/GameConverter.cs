using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
			Type t = typeof(T);
			MethodInfo methodInfo = t.GetMethod("Get", BindingFlags.Static | BindingFlags.Public);
			if (methodInfo != null) return methodInfo.Invoke(null, new string[]{(string)reader.Value});
			else throw new MissingMethodException($"The method 'Get' does not exist on type '{t.FullName}'."); //this should never run but just incase
		}
		else throw new JsonSerializationException("Invalid token type");
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		GameEntity instance = value as GameEntity;
		if (instance != null)
		{
			serializer.Serialize(writer, instance.Id);
		}
		else throw new JsonSerializationException("Value cannot be null");
	}
}
