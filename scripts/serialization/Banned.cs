using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Banned
{
	[JsonProperty("godlike")] public List<GameItem> Godlike { get; set; }
	[JsonProperty("inappropriate")] public List<GameItem> Inappropriate { get; set; }
	[JsonProperty("armorchoices")] public List<GameItem> ArmorChoices { get; set; }
	[JsonProperty("weaponchoices")] public List<GameItem> WeaponChoices { get; set; }
	[JsonProperty("contraband")] public List<GameItem> Contraband { get; set; }
	[JsonProperty("overpowered")] public List<GameItem> Overpowered { get; set; }
}