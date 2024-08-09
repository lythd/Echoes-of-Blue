using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Ammo
{
	[JsonProperty("blunt")]
	public int BluntDamage { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("piercing")]
	public int PiercingDamage { get; set; }
}
