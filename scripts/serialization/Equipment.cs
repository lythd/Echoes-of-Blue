using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.game;
using GodotSteam;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class Equipment
{
	[JsonProperty("main")] public GameItem Main { get; set; } = GameItem.Get("NONE");
	[JsonProperty("armor")] public GameItem Armor { get; set; } = GameItem.Get("NONE");
	[JsonProperty("accessory")] public GameItem Accessory { get; set; } = GameItem.Get("NONE");
	[JsonProperty("ammo")] public GameItem Ammo { get; set; } = GameItem.Get("NONE"); // could be bait too
	[JsonProperty("armorvanity")] public GameItem ArmorVanity { get; set; } = GameItem.Get("NONE");
	[JsonProperty("accessoryvanity")] public GameItem AccessoryVanity { get; set; } = GameItem.Get("NONE");
	[JsonProperty("showarmor")] public bool ShowArmor { get; set; } = true; // whether to show non-vanity armor
	[JsonProperty("showaccessory")] public bool ShowAccessory { get; set; } = true; // whether to show non-vanity accessories
}