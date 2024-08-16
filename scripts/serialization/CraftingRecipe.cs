using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class CraftingRecipe
{
	[JsonProperty("amount")] public int Amount { get; set; }
	[JsonProperty("inputs")] public Dictionary<GameItem, int> Inputs { get; set; }
}