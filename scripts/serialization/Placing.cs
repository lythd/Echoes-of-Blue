using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Placing
{
	[JsonProperty("tile")] public GameTile Tile { get; set; }
}