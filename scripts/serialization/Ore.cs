using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Ore
{
	[JsonProperty("reducedtier")]
	public int ReducedTier { get; set; }
	[JsonProperty("tier")]
	public int Tier { get; set; }
}