using Godot;
using System;

public partial class GameData : Node
{
	private static bool _initialized = false;
	
	public string PlayerLocation { get; set; }
	public string PlayerName { get; set; }
	
	public override void _Ready() {
		if(!_initialized) ResetData();
		_initialized = true;
	}
	
	public void ResetData()
	{
		string[] locations = {"CrossRoads","WitchesSwamp","Haven","Jungle","Rubberport","Smithlands","Mines","Forests","SilkRoad","SkyCity","FishingVille","Mineshaft","Fields","DyronixsLair","DungeonCity","IncendiumKeep","BeastsDen","AshenValley"};
		PlayerLocation = locations[Random.Shared.Next(locations.Length)];
		PlayerName = "Amber";
	}
}