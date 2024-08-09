using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Banned
{
	[JsonProperty("godlike")]
	public List<string> Godlike { get; set; }
	[JsonProperty("inappropriate")]
	public List<string> Inappropriate { get; set; }
	[JsonProperty("armorchoices")]
	public List<string> ArmorChoices { get; set; }
	[JsonProperty("weaponchoices")]
	public List<string> WeaponChoices { get; set; }
	[JsonProperty("contraband")]
	public List<string> Contraband { get; set; }
	[JsonProperty("overpowered")]
	public List<string> Overpowered { get; set; }
}
