using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Bait
{
	[JsonProperty("drop")]
	public string Drop { get; set; }
	[JsonProperty("effects")]
	public List<GameEffect> Effects { get; set; }
	[JsonProperty("fp")]
	public int FishingPower { get; set; }
}
