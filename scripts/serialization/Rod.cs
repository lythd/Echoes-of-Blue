using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Rod
{
	[JsonProperty("bcc")]
	public int BaitConsumptionChance { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("fp")]
	public int FishingPower { get; set; }
	[JsonProperty("lbc")]
	public int LineBreakChance { get; set; }
}
