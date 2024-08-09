using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Food
{
	[JsonProperty("energy")]
	public int Energy { get; set; }
	[JsonProperty("health")]
	public int Health { get; set; }
}
