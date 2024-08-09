using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(StringConverter<Effect>))]
public class Effect
{
	public string Value { get; set; }
}
