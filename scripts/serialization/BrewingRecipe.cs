using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class BrewingRecipe
{
	[JsonProperty("amount")]
	public int Amount { get; set; }
	[JsonProperty("inputs")]
	public Dictionary<string, int> Inputs { get; set; }
}
