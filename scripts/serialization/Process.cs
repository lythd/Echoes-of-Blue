using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using GodotSteam;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Process
{
	[JsonProperty("location")] public GameLocation Location { get; set; }
	[JsonProperty("machine")] public GameItem Machine { get; set; }
	[JsonProperty("outputs")] public Dictionary<GameItem, BigInteger> Outputs { get; set; }
	[JsonProperty("completionpercentage")] public float CompletionPercentage { get; set; }
	[JsonProperty("batches")] public int Batches { get; set; }
}