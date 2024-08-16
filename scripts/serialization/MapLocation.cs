using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using GodotSteam;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class MapLocation
{
	[JsonProperty("conflict")] public string Conflict { get; set; }
	[JsonProperty("controller")] public GameCountry Controller { get; set; }
}