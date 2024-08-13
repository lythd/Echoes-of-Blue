using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Accessory
{
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("effects")]
	public List<GameEffect> Effects { get; set; }
}