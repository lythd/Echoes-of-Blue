using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Placing
{
	[JsonProperty("tile")]
	public GameTile Tile { get; set; }
}
