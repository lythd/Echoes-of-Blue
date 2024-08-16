using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Location
{
	[JsonProperty("conflict_at_start")] public string ConflictAtStart { get; set; }
	[JsonProperty("controller_at_start")] public GameCountry ControllerAtStart { get; set; }
	[JsonProperty("lava")] public bool Lava { get; set; }
	[JsonProperty("air")] public bool Air { get; set; }
	[JsonProperty("sun")] public int Sun { get; set; }
	[JsonProperty("wind")] public int Wind { get; set; }
	[JsonProperty("resources")] public Dictionary<GameItem, List<Range>> Resources { get; set; }
	[JsonProperty("fighting")] public LootTable<GameMob> Fighting { get; set; }
	[JsonProperty("fishing")] public LootTableRange<GameItem> Fishing { get; set; }
	[JsonProperty("mining")] public LootTableRange<GameItem> Mining { get; set; }
	[JsonProperty("ores")] public Dictionary<GameItem, int> Ores { get; set; }
}