using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class IntConverter<T> : JsonConverter where T : class
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(T);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Integer)
		{
			T instance = Activator.CreateInstance<T>();
			PropertyInfo property = typeof(T).GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
			if (property != null)
			{
				property.SetValue(instance, (long)reader.Value);
			}
			return instance;
		}
		throw new JsonSerializationException("Invalid token type");
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		T instance = value as T;
		if (instance != null)
		{
			PropertyInfo property = typeof(T).GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
			if (property != null) serializer.Serialize(writer, (long) property.GetValue(instance));
			else throw new JsonSerializationException("Value cannot be null");
		}
		else throw new JsonSerializationException("Value cannot be null");
	}
}
