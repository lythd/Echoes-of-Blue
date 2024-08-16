using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Tile
{
	[JsonProperty("drop")] public GameItem Drop { get; set; }
	[JsonProperty("count")] public int Count { get; set; }
}