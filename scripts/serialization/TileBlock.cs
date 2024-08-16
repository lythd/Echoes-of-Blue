using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using GodotSteam;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class TileBlock
{
	[JsonProperty("base")] public GameTile[,] Base { get; set; }
	[JsonProperty("main")] public GameTile[,] Main { get; set; }
	[JsonProperty("decoration")] public GameTile[,] Decoration { get; set; }
}