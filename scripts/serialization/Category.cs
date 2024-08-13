using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Category
{
	[JsonProperty("show")]
	public bool Show { get; set; }
}