using System;
using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class User
{
	[JsonProperty("name")] public string Name { get; set; }
	[JsonProperty("bag")] public Dictionary<GameItem, BigInteger> Bag { get; set; } = new();
	[JsonProperty("equipment")] public Equipment Equipment { get; set; } = new();
	[JsonProperty("money")] public BigInteger Money { get; set; } = 100;
	[JsonProperty("energy")] public BigInteger Energy { get; set; } = 10;
	[JsonProperty("health")] public float Health { get; set; } = 20.0f;
	[JsonProperty("gang")] public string Gang { get; set; } = "NONE"; // TODO : make a GameGang and use that instead of string, would also require storing the list of gangs in the save file, or if i decide to hardcode them then a data file for them
	[JsonProperty("location")] public GameLocation Location { get; set; } = GameLocation.Get("SKY_CITY");
	[JsonProperty("area")] public LocationArea Area { get; set; } = LocationArea.Main;
	[JsonProperty("position")] public Coordinate Position { get; set; } = new() { X = 0, Y = 0 };
	[JsonProperty("wins")] public int Wins { get; set; }
	[JsonProperty("losses")] public int Losses { get; set; }
	[JsonProperty("mobkills")] public Dictionary<GameMob, int> MobKills { get; set; } = new();
	[JsonProperty("mobkos")] public Dictionary<GameMob, int> MobKos { get; set; } = new();
	[JsonProperty("victories")] public Dictionary<GameUser, int> Victories { get; set; } = new();
	[JsonProperty("plots")] public Dictionary<GameLocation, Plot> Plots { get; set; } = new();
	[JsonProperty("processes")] public List<Process> Processes { get; set; } = [];
	[JsonProperty("pumpkinseed")] public bool PumpkinSeed { get; set; }
	[JsonProperty("rocketacquired")] public bool RocketAcquired { get; set; }
	[JsonProperty("quests")] public Dictionary<string, int> Quests { get; set; } = new(); // the int represents how many of their quests u've done// TODO : replace string with GameNpc
	[JsonProperty("boycotts")] public List<string> Boycotts { get; set; } = []; // TODO : replace string with GameStall
	[JsonProperty("stalls")] public Dictionary<string, Stall> Stalls { get; set; } = new(); // TODO : replace string with GameStall
	[JsonProperty("lastbeg")] public DateTime LastBeg { get; set; } = DateTime.UnixEpoch;
	[JsonProperty("loans")] public Dictionary<GameUser, Tuple<int, int>> Loans { get; set; } = new();
}