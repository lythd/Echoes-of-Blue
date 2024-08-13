using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Rod
{
	[JsonProperty("bcc")]
	public int BaitConsumptionChance { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("fp")]
	public int FishingPower { get; set; }
	[JsonProperty("lbc")]
	public int LineBreakChance { get; set; }
}