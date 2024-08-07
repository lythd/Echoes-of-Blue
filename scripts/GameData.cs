using Godot;
using System;

public partial class GameData : Node
{
	private static bool _initialized = false;
	
	public string PlayerLocation { get; set; }
	public string PlayerName { get; set; }
	
	public override void _Ready() {
		if(!_initialized) {
			_initialized = true;
			return
		}
		ResetData();
	}
	
	public void ResetData()
	{
		string[] locations = {"CrossRoads","WitchesSwamp","Haven","Jungle","Rubberport","Smithlands","Mines","Forests","SilkRoad","SkyCity","FishingVille","Mineshaft","Fields","DyronixsLair","DungeonCity","IncendiumKeep","BeastsDen","PortCity","AshenValleys"};
		PlayerLocation = locations[Random.Shared.Next(locations.Length)];
		PlayerName = "Amber";
	}
}
