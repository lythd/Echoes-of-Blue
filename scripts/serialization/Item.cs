using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Item
{
	[JsonProperty("starting_amount", DefaultValueHandling = DefaultValueHandling.Ignore)]
	public int StartingAmount { get; set; }
	[JsonProperty("categories")]
	public List<string> Categories { get; set; }
	[JsonProperty("price")]
	public int Price { get; set; }
}
