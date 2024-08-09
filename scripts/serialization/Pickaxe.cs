using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Pickaxe
{
	[JsonProperty("double")]
	public int DoubleOreChance { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("tier")]
	public int Tier { get; set; }
}
