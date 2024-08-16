using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Machine
{
	[JsonProperty("power")] public int Power { get; set; }
	[JsonProperty("consumed")] public Dictionary<GameItem, int> Consumed { get; set; }
	[JsonProperty("extracted")] public Dictionary<GameItem, int> Extracted { get; set; }
	[JsonProperty("produced")] public Dictionary<GameItem, int> Produced { get; set; }
	[JsonProperty("duration")] public int Duration { get; set; }
}