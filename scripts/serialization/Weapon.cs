using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Weapon
{
	[JsonProperty("bdmg")]
	public int BluntDamage { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("pdmg")]
	public int PiercingDamage { get; set; }
	[JsonProperty("priority")]
	public int Priority { get; set; }
}
