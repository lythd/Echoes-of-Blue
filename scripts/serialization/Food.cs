using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Food
{
	[JsonProperty("energy")] public int Energy { get; set; }
	[JsonProperty("health")] public int Health { get; set; }
}