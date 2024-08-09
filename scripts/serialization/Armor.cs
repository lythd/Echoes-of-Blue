using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Armor
{
	[JsonProperty("defense")]
	public int Defense { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("effects")]
	public List<GameEffect> Effects { get; set; }
	[JsonProperty("priority")]
	public int Priority { get; set; }
}
