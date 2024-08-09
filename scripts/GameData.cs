using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class GameData : Node
{
	private static bool _initialized = false;
	
	public string PlayerLocation { get; set; }
	public string PlayerName { get; set; }
	
	public override void _Ready() {
		if(_initialized) return;
		_initialized = true;
		ResetData();
		var loaded = LoadDict<Location>("locations");
		GD.Print($"Json: `{JsonConvert.SerializeObject(loaded, Formatting.Indented)}`");
		//var loadedSingle = JsonConvert.DeserializeObject<Range>("4");
		//GD.Print($"Json: `{JsonConvert.SerializeObject(loadedSingle, Formatting.Indented)}`, Value: `{loadedSingle.Value}`, Max: `{loadedSingle.MaxValue}`, Min: `{loadedSingle.MinValue}`");
		//var loadedRange = JsonConvert.DeserializeObject<Range>("[3, 6]");
		//GD.Print($"Json: `{JsonConvert.SerializeObject(loadedRange, Formatting.Indented)}`, Value: `{loadedRange.Value}`, Max: `{loadedRange.MaxValue}`, Min: `{loadedRange.MinValue}`");
	}
	
	public T LoadWhole<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<T>(fileContent);
	}
	
	public Dictionary<string, T> LoadDict<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<Dictionary<string, T>>(fileContent);
	}
	
	public List<T> LoadList<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<List<T>>(fileContent);
	}
	
	public void ResetData()
	{
		string[] locations = {"CrossRoads","WitchesSwamp","Haven","Jungle","Rubberport","Smithlands","Mines","Forests","SilkRoad","SkyCity","FishingVille","Mineshaft","Fields","DyronixsLair","DungeonCity","IncendiumKeep","BeastsDen","PortCity","AshenValleys"};
		PlayerLocation = locations[Random.Shared.Next(locations.Length)];
		PlayerName = Tr("DEFAULT_NAME");
	}
}
