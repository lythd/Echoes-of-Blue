using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Ore
{
	[JsonProperty("reducedtier")]
	public int ReducedTier { get; set; }
	[JsonProperty("tier")]
	public int Tier { get; set; }
}
