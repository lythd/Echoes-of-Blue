using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Category
{
	[JsonProperty("show")]
	public bool Show { get; set; }
}
