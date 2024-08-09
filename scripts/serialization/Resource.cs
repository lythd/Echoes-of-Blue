using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(IntConverter<Resource>))]
public class Resource
{
	public long Value { get; set; }
}
