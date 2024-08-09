using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Mob
{
	[JsonProperty("type")]
	public string Type { get; set; }
	[JsonProperty("pdmg")]
	public int PiercingDamage { get; set; }
	[JsonProperty("bdmg")]
	public int BluntDamage { get; set; }
	[JsonProperty("mhp")]
	public int MaximumHealth { get; set; }
	[JsonProperty("def")]
	public int Defense { get; set; }
	[JsonProperty("spd")]
	public int Speed { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("drops")]
	public Dictionary<string, Dictionary<GameItem, Range>> Drops { get; set; }
}
