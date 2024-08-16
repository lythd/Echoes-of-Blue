using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using GodotSteam;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Plot
{
	[JsonProperty("machines")] public Dictionary<GameItem, List<Coordinate>> Machines { get; set; }
	[JsonProperty("plots")] public Dictionary<CoordinateI, TileBlock> Plots { get; set; }
	[JsonProperty("resources")] public Dictionary<GameItem, BigInteger> Resources { get; set; }
}