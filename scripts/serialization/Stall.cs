using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using GodotSteam;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Stall
{
	[JsonProperty("item")] public GameItem Item { get; set; }
	[JsonProperty("price")] public int Price { get; set; }
	[JsonProperty("slogan")] public string Slogan { get; set; }
	[JsonProperty("gangonly")] public bool GangOnly { get; set; }
	[JsonProperty("stock")] public BigInteger Stock { get; set; }
}