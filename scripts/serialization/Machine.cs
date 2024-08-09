using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Machine
{
	[JsonProperty("power")]
	public int Power { get; set; }
	[JsonProperty("consumed")]
	public Dictionary<string, int> Consumed { get; set; }
	[JsonProperty("extracted")]
	public Dictionary<string, int> Extracted { get; set; }
	[JsonProperty("produced")]
	public Dictionary<string, int> Produced { get; set; }
	[JsonProperty("duration")]
	public int Duration { get; set; }
}
