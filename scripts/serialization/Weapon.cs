using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Weapon
{
	[JsonProperty("bdmg")]
	public int BluntDamage { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("effects")]
	public List<GameEffect> Effects { get; set; }
	[JsonProperty("pdmg")]
	public int PiercingDamage { get; set; }
	[JsonProperty("priority")]
	public int Priority { get; set; }
}