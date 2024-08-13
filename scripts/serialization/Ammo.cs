using System.Collections.Generic;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Ammo
{
	[JsonProperty("blunt")]
	public int BluntDamage { get; set; }
	[JsonProperty("effects")]
	public List<GameEffect> Effects { get; set; }
	[JsonProperty("piercing")]
	public int PiercingDamage { get; set; }
}