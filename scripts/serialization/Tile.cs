using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Tile
{
	[JsonProperty("drop")]
	public GameItem Drop { get; set; }
	[JsonProperty("count")]
	public int Count { get; set; }
}
