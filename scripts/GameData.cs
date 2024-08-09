using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class GameData : Node
{
	private static bool _initialized = false;
	public static GameData Instance { get; private set; }
	
	public string PlayerLocation { get; set; }
	public string PlayerName { get; set; }
	
	public JsonSerializerSettings settings = new JsonSerializerSettings
	{
		Converters = new List<JsonConverter>
		{
			new JsonCustomKeyDictionaryObjectConverter()
		}
	};
	
	public override void _Ready() {
		if(_initialized) return;
		Instance = this;
		_initialized = true;
		var loaded = LoadDict<Mob>("mobs");
		GD.Print($"Json: `{JsonConvert.SerializeObject(loaded, Formatting.Indented, settings)}`");
		//GD.Print($"Name: {loaded[0].Name}, Desc: {loaded[0].Desc}");
		//var loadedSingle = JsonConvert.DeserializeObject<Range>("4");
		//GD.Print($"Json: `{JsonConvert.SerializeObject(loadedSingle, Formatting.Indented)}`, Value: `{loadedSingle.Value}`, Max: `{loadedSingle.MaxValue}`, Min: `{loadedSingle.MinValue}`");
		//var loadedRange = JsonConvert.DeserializeObject<Range>("[3, 6]");
		//GD.Print($"Json: `{JsonConvert.SerializeObject(loadedRange, Formatting.Indented)}`, Value: `{loadedRange.Value}`, Max: `{loadedRange.MaxValue}`, Min: `{loadedRange.MinValue}`");
	
		ResetData();
	}
	
	public T LoadWhole<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<T>(fileContent, settings);
	}
	
	public Dictionary<string, T> LoadDict<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<Dictionary<string, T>>(fileContent, settings);
	}
	
	public List<T> LoadList<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<List<T>>(fileContent, settings);
	}
	
	public void ResetData()
	{
		string[] locations = {"CrossRoads","WitchesSwamp","Haven","Jungle","Rubberport","Smithlands","Mines","Forests","SilkRoad","SkyCity","FishingVille","Mineshaft","Fields","DyronixsLair","DungeonCity","IncendiumKeep","BeastsDen","PortCity","AshenValleys"};
		PlayerLocation = locations[Random.Shared.Next(locations.Length)];
		PlayerName = Tr("DEFAULT_NAME");
	}
}
