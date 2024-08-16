using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using GodotSteam;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Save
{
	[JsonProperty("name")] public string Name { get; set; }
	[JsonProperty("users")] public Dictionary<GameUser, User> Users { get; set; } = new();
	[JsonProperty("marketchanges")] public Dictionary<GameItem, BigInteger> MarketChanges { get; set; } = new();
	[JsonProperty("map")] public Dictionary<GameLocation, MapLocation> Map { get; set; } = new();
	[JsonProperty("skeletonkingdefeated")] public bool SkeletonKingDefeated { get; set; }
	[JsonProperty("dyronixdefeated")] public bool DyronixDefeated { get; set; }
	[JsonProperty("jimmyprogression")] public int JimmyProgression { get; set; }
	[JsonProperty("tincrisis")] public int TinCrisis { get; set; }
}