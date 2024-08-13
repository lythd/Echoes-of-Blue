using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Pickaxe
{
	[JsonProperty("double")]
	public int DoubleOreChance { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("tier")]
	public int Tier { get; set; }
}