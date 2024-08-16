using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Mob
{
	[JsonProperty("type")] public string Type { get; set; }
	[JsonProperty("pdmg")] public int PiercingDamage { get; set; }
	[JsonProperty("bdmg")] public int BluntDamage { get; set; }
	[JsonProperty("mhp")] public int MaximumHealth { get; set; }
	[JsonProperty("def")] public int Defense { get; set; }
	[JsonProperty("spd")] public int Speed { get; set; }
	[JsonProperty("effects")] public List<GameEffect> Effects { get; set; }
	[JsonProperty("drops")] public LootTableRange<GameItem> Drops { get; set; }
}