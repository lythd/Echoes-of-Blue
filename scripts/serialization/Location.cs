using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Location
{
	[JsonProperty("conflict_at_start")]
	public string ConflictAtStart { get; set; }
	[JsonProperty("controller_at_start")]
	public string ControllerAtStart { get; set; }
	[JsonProperty("lava")]
	public bool Lava { get; set; }
	[JsonProperty("air")]
	public bool Air { get; set; }
	[JsonProperty("sun")]
	public int Sun { get; set; }
	[JsonProperty("wind")]
	public int Wind { get; set; }
	[JsonProperty("resources")]
	public Dictionary<string, List<Range>> Resources { get; set; }
	[JsonProperty("fighting")]
	public Dictionary<string, List<string>> Fighting { get; set; }
	[JsonProperty("fishing")]
	public Dictionary<string, Dictionary<string, Range>> Fishing { get; set; }
	[JsonProperty("mining")]
	public Dictionary<string, Dictionary<string, Range>> Mining { get; set; }
}