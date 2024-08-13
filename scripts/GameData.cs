using System;
using System.Collections.Generic;
using System.Linq;
using EchoesofBlue.scripts.game;
using EchoesofBlue.scripts.serialization;
using EchoesofBlue.scripts.stuff;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts;

public partial class GameData : Node
{
	private static bool _initialized = false;
	public static GameData Instance { get; private set; }
	
	public GameLocation PlayerLocation { get; set; }
	public string PlayerName { get; set; }
	
	private Dictionary<GameItem, Accessory> _accessories;
	private Dictionary<GameItem, Ammo> _ammos;
	private Dictionary<GameItem, Armor> _armors;
	private Dictionary<GameItem, Bait> _baits;
	private Banned _banned;
	private Dictionary<GameItem, BrewingRecipe> _brewingRecipes;
	private Dictionary<GameCategory, Category> _categories;
	private List<GameCountry> _countries;
	private Dictionary<GameItem, CraftingRecipe> _craftingRecipes;
	private List<GameEffect> _effects;
	private Dictionary<GameItem, Food> _foods;
	private Dictionary<GameItem, Item> _items;
	private Dictionary<GameLocation, Location> _locations;
	private Dictionary<GameItem, Machine> _machines;
	private Dictionary<GameMob, Mob> _mobs;
	private Dictionary<GameItem, Ore> _ores;
	private Dictionary<GameItem, Pickaxe> _pickaxes;
	private Dictionary<GameItem, Placing> _placings;
	private Dictionary<GameItem, Rod> _rods;
	private Dictionary<GameTile, Tile> _tiles;
	private Dictionary<GameItem, Weapon> _weapons;
	
	public Accessory GetAccessory(GameItem key) => _accessories.GetValueOrDefault(key);
	public Ammo GetAmmo(GameItem key) => _ammos.GetValueOrDefault(key);
	public Armor GetArmor(GameItem key) => _armors.GetValueOrDefault(key);
	public Bait GetBait(GameItem key) => _baits.GetValueOrDefault(key);
	public Banned GetBanned() => _banned;
	public BrewingRecipe GetBrewingRecipe(GameItem key) => _brewingRecipes.GetValueOrDefault(key);
	public Category GetCategory(GameCategory key) => _categories.GetValueOrDefault(key);
	public bool HasCountry(GameCountry key) => _countries.Contains(key);
	public CraftingRecipe GetCraftingRecipe(GameItem key) => _craftingRecipes.GetValueOrDefault(key);
	public bool HasEffect(GameEffect key) => _effects.Contains(key);
	public Food GetFood(GameItem key) => _foods.GetValueOrDefault(key);
	public Item GetItem(GameItem key) => _items.GetValueOrDefault(key);
	public Location GetLocation(GameLocation key) => _locations.GetValueOrDefault(key);
	public Machine GetMachine(GameItem key) => _machines.GetValueOrDefault(key);
	public Mob GetMob(GameMob key) => _mobs.GetValueOrDefault(key);
	public Ore GetOre(GameItem key) => _ores.GetValueOrDefault(key);
	public Pickaxe GetPickaxe(GameItem key) => _pickaxes.GetValueOrDefault(key);
	public Placing GetPlacing(GameItem key) => _placings.GetValueOrDefault(key);
	public Rod GetRod(GameItem key) => _rods.GetValueOrDefault(key);
	public Tile GetTile(GameTile key) => _tiles.GetValueOrDefault(key);
	public Weapon GetWeapon(GameItem key) => _weapons.GetValueOrDefault(key);
	
	public readonly JsonSerializerSettings Settings = new()
	{
		Converters = new List<JsonConverter>
		{
			new JsonCustomKeyDictionaryObjectConverter()
		}
	};
	
	public override void _Ready() {
		if(_initialized) return;
		Instance = this;
		_initialized = true;
		_accessories = LoadDict<GameItem, Accessory>("accessories");
		_ammos = LoadDict<GameItem, Ammo>("ammos");
		_armors = LoadDict<GameItem, Armor>("armors");
		_baits = LoadDict<GameItem, Bait>("baits");
		_banned = LoadWhole<Banned>("banned");
		_brewingRecipes = LoadDict<GameItem, BrewingRecipe>("brewingrecipes");
		_categories = LoadDict<GameCategory, Category>("categories");
		_countries = LoadList<GameCountry>("countries");
		_craftingRecipes = LoadDict<GameItem, CraftingRecipe>("craftingrecipes");
		_effects = LoadList<GameEffect>("effects");
		_foods = LoadDict<GameItem, Food>("foods");
		_items = LoadDict<GameItem, Item>("items");
		_locations = LoadDict<GameLocation, Location>("locations");
		_machines = LoadDict<GameItem, Machine>("machines");
		_mobs = LoadDict<GameMob, Mob>("mobs");
		_ores = LoadDict<GameItem, Ore>("ores");
		_pickaxes = LoadDict<GameItem, Pickaxe>("pickaxes");
		_placings = LoadDict<GameItem, Placing>("placings");
		_rods = LoadDict<GameItem, Rod>("rods");
		_tiles = LoadDict<GameTile, Tile>("tiles");
		_weapons = LoadDict<GameItem, Weapon>("weapons");
		ResetData();
	}
	
	public T LoadWhole<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<T>(fileContent, Settings);
	}
	
	public Dictionary<string, T> LoadDict<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<Dictionary<string, T>>(fileContent, Settings);
	}
	
	public Dictionary<Tk, Tv> LoadDict<Tk,Tv>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<Dictionary<Tk, Tv>>(fileContent, Settings);
	}
	
	public List<T> LoadList<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<List<T>>(fileContent, Settings);
	}
	
	public void ResetData()
	{
		GameLocation[] locations = _locations.Keys.ToArray();
		PlayerLocation = locations[Random.Shared.Next(locations.Length)];
		PlayerName = Tr("DEFAULT_NAME");
	}
}