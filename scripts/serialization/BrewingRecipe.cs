using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class BrewingRecipe
{
	[JsonProperty("amount")] public int Amount { get; set; }
	[JsonProperty("inputs")] public Dictionary<GameItem, int> Inputs { get; set; }
}