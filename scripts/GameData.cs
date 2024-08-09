using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

public partial class GameData : Node
{
	private static bool _initialized = false;
	
	public string PlayerLocation { get; set; }
	public string PlayerName { get; set; }
	
	public override void _Ready() {
		if(_initialized) return;
		_initialized = true;
		ResetData();
		var loaded = LoadList<Country>("countries");
		GD.Print($"Json: `{JsonConvert.SerializeObject(loaded, Formatting.Indented)}`");
	}
	
	public T LoadWhole<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<T>(fileContent);
	}
	
	public Dictionary<string, T> LoadDict<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<Dictionary<string, T>>(fileContent);
	}
	
	public List<T> LoadList<T>(string name) {
		var file = FileAccess.Open($"res://data/{name}.json", FileAccess.ModeFlags.Read);
		var fileContent = file.GetAsText();
		file.Close();
		return JsonConvert.DeserializeObject<List<T>>(fileContent);
	}
	
	public void ResetData()
	{
		string[] locations = {"CrossRoads","WitchesSwamp","Haven","Jungle","Rubberport","Smithlands","Mines","Forests","SilkRoad","SkyCity","FishingVille","Mineshaft","Fields","DyronixsLair","DungeonCity","IncendiumKeep","BeastsDen","PortCity","AshenValleys"};
		PlayerLocation = locations[Random.Shared.Next(locations.Length)];
		PlayerName = Tr("DEFAULT_NAME");
	}
}

public class StringConverter<T> : JsonConverter where T : class
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(T);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.String)
		{
			T instance = Activator.CreateInstance<T>();
			PropertyInfo property = typeof(T).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public);
			if (property != null)
			{
				property.SetValue(instance, (string)reader.Value);
			}
			return instance;
		}
		throw new JsonSerializationException("Invalid token type");
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		T instance = value as T;
		if (instance != null)
		{
			PropertyInfo property = typeof(T).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public);
			if (property != null) serializer.Serialize(writer, (string) property.GetValue(instance));
			else throw new JsonSerializationException("Value cannot be null");
		}
		else throw new JsonSerializationException("Value cannot be null");
	}
}

public class Accessory
{
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
}

public class Ammo
{
	[JsonProperty("blunt")]
	public int BluntDamage { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("piercing")]
	public int PiercingDamage { get; set; }
}

public class Armor
{
	[JsonProperty("defense")]
	public int Defense { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("priority")]
	public int Priority { get; set; }
}

public class Bait
{
	[JsonProperty("drop")]
	public string Drop { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("fp")]
	public int FishingPower { get; set; }
}

public class Banned
{
	[JsonProperty("godlike")]
	public List<string> Godlike { get; set; }
	[JsonProperty("inappropriate")]
	public List<string> Inappropriate { get; set; }
	[JsonProperty("armorchoices")]
	public List<string> ArmorChoices { get; set; }
	[JsonProperty("weaponchoices")]
	public List<string> WeaponChoices { get; set; }
	[JsonProperty("contraband")]
	public List<string> Contraband { get; set; }
	[JsonProperty("overpowered")]
	public List<string> Overpowered { get; set; }
}

public class BrewingRecipe
{
	[JsonProperty("amount")]
	public int Amount { get; set; }
	[JsonProperty("inputs")]
	public Dictionary<string, int> Inputs { get; set; }
}

public class Category
{
	[JsonProperty("show")]
	public bool Show { get; set; }
}

[JsonConverter(typeof(StringConverter<Country>))]
public class Country
{
	public string Name { get; set; }
}

public class CraftingRecipe
{
	[JsonProperty("amount")]
	public int Amount { get; set; }
	[JsonProperty("inputs")]
	public Dictionary<string, int> Inputs { get; set; }
}

public class Effect
{
	public string Name { get; set; }
}

public class Food
{
	[JsonProperty("energy")]
	public int Energy { get; set; }
	[JsonProperty("health")]
	public int Health { get; set; }
}

public class Item
{
	[JsonProperty("starting_amount")]
	public int StartingAmount { get; set; }
	[JsonProperty("categories")]
	public List<string> Categories { get; set; }
	[JsonProperty("price")]
	public int Price { get; set; }
}

public class Location
{
	[JsonProperty("conflict_at_start")]
	public string ConflictAtStart { get; set; }
	[JsonProperty("controller_at_start")]
	public string ControllerAtStart { get; set; }
	[JsonProperty("lava")]
	public bool Lava { get; set; }
	[JsonProperty("air")]
	public bool Air { get; set; }
	[JsonProperty("sun")]
	public int Sun { get; set; }
	[JsonProperty("wind")]
	public int Wind { get; set; }
	[JsonProperty("resources")]
	public Dictionary<string, List<int>> Resources { get; set; }
	[JsonProperty("fighting")]
	public Dictionary<string, List<string>> Fighting { get; set; }
	[JsonProperty("fishing")]
	public Dictionary<string, Dictionary<string, List<int>>> Fishing { get; set; }
	[JsonProperty("mining")]
	public Dictionary<string, Dictionary<string, List<int>>> Mining { get; set; }
}

public class Machine
{
	[JsonProperty("power")]
	public int Power { get; set; }
	[JsonProperty("consumed")]
	public Dictionary<string, int> Consumed { get; set; }
	[JsonProperty("extracted")]
	public Dictionary<string, int> Extracted { get; set; }
	[JsonProperty("produced")]
	public Dictionary<string, int> Produced { get; set; }
	[JsonProperty("duration")]
	public int Duration { get; set; }
}

public class Mob
{
	[JsonProperty("type")]
	public string Type { get; set; }
	[JsonProperty("pdmg")]
	public int PiercingDamage { get; set; }
	[JsonProperty("bdmg")]
	public int BluntDamage { get; set; }
	[JsonProperty("mhp")]
	public int MaximumHealth { get; set; }
	[JsonProperty("def")]
	public int Defense { get; set; }
	[JsonProperty("spd")]
	public int Speed { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("drops")]
	public Dictionary<string, Dictionary<string, List<int>>> Drops { get; set; }
}

public class Ore
{
	[JsonProperty("reducedtier")]
	public int ReducedTier { get; set; }
	[JsonProperty("tier")]
	public int Tier { get; set; }
}

public class Pickaxe
{
	[JsonProperty("double")]
	public int DoubleOreChance { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("tier")]
	public int Tier { get; set; }
}

public class Placing
{
	[JsonProperty("tile")]
	public string Tile { get; set; }
}

public class Resource
{
	public int Number { get; set; }
}

public class Rod
{
	[JsonProperty("bcc")]
	public int BaitConsumptionChance { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("fp")]
	public int FishingPower { get; set; }
	[JsonProperty("lbc")]
	public int LineBreakChance { get; set; }
}

public class Tile
{
	[JsonProperty("drop")]
	public string Drop { get; set; }
	[JsonProperty("count")]
	public int Count { get; set; }
}

public class Weapon
{
	[JsonProperty("bdmg")]
	public int BluntDamage { get; set; }
	[JsonProperty("durability")]
	public int Durability { get; set; }
	[JsonProperty("effects")]
	public List<string> Effects { get; set; }
	[JsonProperty("pdmg")]
	public int PiercingDamage { get; set; }
	[JsonProperty("priority")]
	public int Priority { get; set; }
}
