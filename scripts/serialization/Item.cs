using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Item
{
	[JsonProperty("starting_amount")]
	public int StartingAmount { get; set; }
	[JsonProperty("categories")]
	public List<GameCategory> Categories { get; set; }
	[JsonProperty("price")]
	public int Price { get; set; }
}