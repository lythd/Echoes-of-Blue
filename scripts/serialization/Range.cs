using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(RangeConverter))]
public class Range
{
	public long SingleValue { get; set; }
	public List<long> RangeValues { get; set; }
	public bool IsRange { get; set; }
	
	public long Value
	{
		get {
			if(IsRange) return new Random().NextLong(RangeValues[0], RangeValues[1]+1);
			return SingleValue;
		}
		
		private set {}
	}
	
	public long MaxValue
	{
		get {
			if(IsRange) return RangeValues[1];
			return SingleValue;
		}
		
		private set {}
	}
	
	public long MinValue
	{
		get {
			if(IsRange) return RangeValues[0];
			return SingleValue;
		}
		
		private set {}
	}
}
