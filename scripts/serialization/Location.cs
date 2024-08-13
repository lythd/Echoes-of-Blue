using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Location
{
	[JsonProperty("conflict_at_start")]
	public string ConflictAtStart { get; set; }
	[JsonProperty("controller_at_start")]
	public GameCountry ControllerAtStart { get; set; }
	[JsonProperty("lava")]
	public bool Lava { get; set; }
	[JsonProperty("air")]
	public bool Air { get; set; }
	[JsonProperty("sun")]
	public int Sun { get; set; }
	[JsonProperty("wind")]
	public int Wind { get; set; }
	[JsonProperty("resources")]
	public Dictionary<GameItem, List<Range>> Resources { get; set; }
	[JsonProperty("fighting")]
	public Dictionary<string, List<GameMob>> Fighting { get; set; }
	[JsonProperty("fishing")]
	public Dictionary<string, Dictionary<GameItem, Range>> Fishing { get; set; }
	[JsonProperty("mining")]
	public Dictionary<string, Dictionary<GameItem, Range>> Mining { get; set; }
}