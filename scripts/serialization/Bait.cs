using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Bait
{
	[JsonProperty("drop")]
	public GameItem Drop { get; set; }
	[JsonProperty("effects")]
	public List<GameEffect> Effects { get; set; }
	[JsonProperty("fp")]
	public int FishingPower { get; set; }
}