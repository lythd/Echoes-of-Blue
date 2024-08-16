using System;
using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class User
{
	[JsonProperty("name")] public string Name { get; set; }
	[JsonProperty("bag")] public Dictionary<GameItem, BigInteger> Bag { get; set; }
	[JsonProperty("equipment")] public Equipment Equipment { get; set; }
	[JsonProperty("money")] public BigInteger Money { get; set; }
	[JsonProperty("energy")] public BigInteger Energy { get; set; }
	[JsonProperty("gang")] public string Gang { get; set; } // TODO : make a GameGang and use that instead of string, would also require storing the list of gangs in the save file, or if i decide to hardcode them then a data file for them
	[JsonProperty("location")] public GameLocation Location { get; set; }
	[JsonProperty("area")] public LocationArea Area { get; set; }
	[JsonProperty("position")] public Coordinate Position { get; set; }
	[JsonProperty("wins")] public int Wins { get; set; }
	[JsonProperty("losses")] public int Losses { get; set; }
	[JsonProperty("mobkills")] public Dictionary<GameMob, int> MobKills { get; set; }
	[JsonProperty("mobkos")] public Dictionary<GameMob, int> MobKos { get; set; }
	[JsonProperty("victories")] public Dictionary<GameUser, int> Victories { get; set; }
	[JsonProperty("plots")] public Dictionary<GameLocation, Plot> Plots { get; set; }
	[JsonProperty("processes")] public List<Process> Processes { get; set; }
	[JsonProperty("pumpkinseed")] public bool PumpkinSeed { get; set; }
	[JsonProperty("rocketacquired")] public bool RocketAcquired { get; set; }
	[JsonProperty("quests")] public Dictionary<string, int> Quests { get; set; } // the int represents how many of their quests u've done// TODO : replace string with GameNpc
	[JsonProperty("boycotts")] public List<string> Boycotts { get; set; } // TODO : replace string with GameStall
	[JsonProperty("stalls")] public Dictionary<string, Stall> Stalls { get; set; } // TODO : replace string with GameStall
	[JsonProperty("lastbeg")] public DateTime LastBeg { get; set; }
	[JsonProperty("loans")] public Dictionary<GameUser, Tuple<int, int>> Loans { get; set; }
}