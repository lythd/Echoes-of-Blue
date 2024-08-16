using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using EchoesofBlue.scripts.game;
using EchoesofBlue.scripts.serialization;
using EchoesofBlue.scripts.stuff;
using Godot;
using Newtonsoft.Json;
using FileAccess = Godot.FileAccess;
using Vector2 = Godot.Vector2;

namespace EchoesofBlue.scripts;

public partial class GameData : Node
{
	private static bool _initialized;

	private static GameData _instance;
	public static GameData Instance
	{
		get => _initialized ? _instance : new GameData();
		private set => _instance = value;
	}
	
	private Save _save;
	
	private Dictionary<GameItem, Accessory> _accessories;
	private Dictionary<GameItem, Ammo> _ammos;
	private Dictionary<GameItem, Armor> _armors;
	private Dictionary<GameItem, Bait> _baits;
	private Banned _banned;
	private LootTableRange<GameItem> _beg;
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

	public string SaveName { get; set; } = "save";

	public string Save
	{
		get => _save is null ? "" : JsonConvert.SerializeObject(_save, Formatting.Indented, Settings);
		set
		{
			if(value is not "") _save = JsonConvert.DeserializeObject<Save>(value, Settings);
		}
	}
	
	public DateTime LastSave = DateTime.UnixEpoch;

	public User GetUser(GameUser key) => _save.Users.GetValueOrDefault(key);
	public BigInteger GetMarketChange(GameItem key) => _save.MarketChanges.GetValueOrDefault(key);
	public MapLocation GetMapLocation(GameLocation key) => _save.Map.GetValueOrDefault(key);
	public bool SkeletonKingDefeated => _save.SkeletonKingDefeated;
	public bool DyronixDefeated => _save.DyronixDefeated;
	public int JimmyProgression => _save.JimmyProgression;
	public int TinCrisis => _save.TinCrisis;

	public Vector2 GetPlayerPosition(string playerId) => GameUser.Get(playerId).Position;
	public int GetPlayerHealth(string playerId) => GameUser.Get(playerId).Health;
	public GameLocation GetPlayerLocation(string playerId) => GameUser.Get(playerId).Location;
	public string GetPlayerName(string playerId) => GameUser.Get(playerId).PlayerName;
	public bool HasPlayerData(string playerId) => GameUser.Get(playerId).Exists;
	public void SetPlayerPosition(string playerId, Vector2 position) => GameUser.Get(playerId).Position = position;
	public void SetPlayerHealth(string playerId, int health) => GameUser.Get(playerId).Health = health;
	public void SetPlayerLocation(string playerId, string location) => GameUser.Get(playerId).Location = GameLocation.Get(location);
	public void SetPlayerName(string playerId, string name) => GameUser.Get(playerId).PlayerName = name;
	public void AddPlayer(string playerId, string name) =>
		_save.Users[GameUser.Get(playerId)] = new() {Name = name, Location = _locations.Keys.ToArray()[Random.Shared.Next(_locations.Keys.ToArray().Length)]};
    
	public Accessory GetAccessory(GameItem key) => _accessories.GetValueOrDefault(key);
	public Ammo GetAmmo(GameItem key) => _ammos.GetValueOrDefault(key);
	public Armor GetArmor(GameItem key) => _armors.GetValueOrDefault(key);
	public Bait GetBait(GameItem key) => _baits.GetValueOrDefault(key);
	public Banned GetBanned() => _banned;
	public LootTableRange<GameItem> GetBeg() => _beg;
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
	
	public static readonly JsonSerializerSettings Settings = new()
	{
		Converters = new List<JsonConverter>
		{
			new JsonCustomKeyDictionaryObjectConverter(),
			new BigIntegerConverter()
		}
	};
	
	public GameData()
	{
		if(_initialized) return;
		Instance = this;
		_initialized = true;
		GD.Print($"Save file exists: {SaveFileExists(SaveName)}");
		_save = SaveFileExists(SaveName) ? LoadSaveWhole<Save>(SaveName) : new Save {Name = SaveName};
		_accessories = LoadDict<GameItem, Accessory>("accessories");
		_ammos = LoadDict<GameItem, Ammo>("ammos");
		_armors = LoadDict<GameItem, Armor>("armors");
		_baits = LoadDict<GameItem, Bait>("baits");
		_banned = LoadWhole<Banned>("banned");
		_beg = LoadWhole<LootTableRange<GameItem>>("beg");
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
	}

	public void SaveGame()
	{
		if ((DateTime.UtcNow - LastSave).TotalSeconds < 10) return;
		LastSave = DateTime.UtcNow;
		if (!DirAccess.DirExistsAbsolute("user://saves")) DirAccess.MakeDirAbsolute("user://saves");
		using var file = FileAccess.Open($"user://saves/{_save.Name}.json", FileAccess.ModeFlags.Write);
		file.StoreString(Save);
		file.Close();
		GD.Print("Saving!");
	}
	
	public static bool SaveFileExists(string name) => FileAccess.FileExists($"user://saves/{name}.json");
    
	public static T LoadSaveWhole<T>(string name) {
		var file = FileAccess.Open($"user://saves/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<T>(fileContent, Settings);
	}

	public static bool FileExists(string name)
	{
		return File.Exists($"res://data/{name}.json");
	}
    
	public static T LoadWhole<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<T>(fileContent, Settings);
	}
	
	public static Dictionary<string, T> LoadDict<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<Dictionary<string, T>>(fileContent, Settings);
	}
	
	public static Dictionary<Tk, Tv> LoadDict<Tk,Tv>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<Dictionary<Tk, Tv>>(fileContent, Settings);
	}
	
	public static List<T> LoadList<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<List<T>>(fileContent, Settings);
	}
	
	/*public void ResetData()
	{
		GameLocation[] locations = _locations.Keys.ToArray();
		PlayerLocation = locations[Random.Shared.Next(locations.Length)];
		PlayerName = Tr("DEFAULT_NAME");
	}*/
}