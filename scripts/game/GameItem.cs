using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameItem>))]
public class GameItem : GameEntity
{
	private GameItem(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameItem> _instances = new Dictionary<string, GameItem>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_ITEM_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_ITEM_DESC"); }
		
		protected set {}
	}
	
	public static GameItem Get(string id) {
		if (!_instances.TryGetValue(id, out GameItem item))
		{
			item = new GameItem(id);
			_instances[id] = item;
		}

		return item;
	}
	
	public List<GameEffect> Effects { get => (GameData.Instance.GetAccessory(this)?.Effects ?? new List<GameEffect>()).Concat(GameData.Instance.GetAmmo(this)?.Effects ?? new List<GameEffect>()).Concat(GameData.Instance.GetArmor(this)?.Effects ?? new List<GameEffect>()).Concat(GameData.Instance.GetWeapon(this)?.Effects ?? new List<GameEffect>()).ToList(); private set {} }
	
	public int Durability { get => GameData.Instance.GetAccessory(this)?.Durability ?? GameData.Instance.GetArmor(this)?.Durability ?? GameData.Instance.GetPickaxe(this)?.Durability ?? GameData.Instance.GetRod(this)?.Durability ?? GameData.Instance.GetWeapon(this)?.Durability ?? -1; private set {} }
	public int BluntDamage { get => GameData.Instance.GetAmmo(this)?.BluntDamage ?? GameData.Instance.GetWeapon(this)?.BluntDamage ?? -1; private set {} }
	public int PiercingDamage { get => GameData.Instance.GetAmmo(this)?.PiercingDamage ?? GameData.Instance.GetWeapon(this)?.PiercingDamage ?? -1; private set {} }
	public int Priority { get => GameData.Instance.GetArmor(this)?.Priority ?? GameData.Instance.GetWeapon(this)?.Priority ?? 0; private set {} }
	public int FishingPower { get => GameData.Instance.GetBait(this)?.FishingPower ?? GameData.Instance.GetRod(this)?.FishingPower ?? -1; private set {} }
	public Dictionary<GameItem, int> Inputs { get => GameData.Instance.GetBrewingRecipe(this)?.Inputs ?? GameData.Instance.GetCraftingRecipe(this)?.Inputs ?? new Dictionary<GameItem, int>(); private set {} }
	public int Amount { get => GameData.Instance.GetBrewingRecipe(this)?.Amount ?? GameData.Instance.GetCraftingRecipe(this)?.Amount ?? 0; private set {} }
	public int Tier { get => GameData.Instance.GetOre(this)?.Tier ?? GameData.Instance.GetPickaxe(this)?.Tier ?? -1; private set {} }
	
	public bool IsAccessory { get => GameData.Instance.GetAccessory(this)!=null; private set {} }
	
	public bool IsAmmo { get => GameData.Instance.GetAmmo(this)!=null; private set {} }
	
	public bool IsArmor { get => GameData.Instance.GetArmor(this)!=null; private set {} }
	public int Defense { get => GameData.Instance.GetArmor(this)?.Defense ?? -1; private set {} }
	
	public bool IsBait { get => GameData.Instance.GetBait(this)!=null; private set {} }
	public GameItem Drop { get => GameData.Instance.GetBait(this)?.Drop ?? GameItem.Get("NONE"); private set {} }
	
	public bool IsGodlike { get => GameData.Instance.GetBanned().Godlike.Contains(this); private set {} }
	public bool IsInappropriate { get => GameData.Instance.GetBanned().Inappropriate.Contains(this); private set {} }
	public bool IsArmorChoice { get => GameData.Instance.GetBanned().ArmorChoices.Contains(this); private set {} }
	public bool IsWeaponChoice { get => GameData.Instance.GetBanned().WeaponChoices.Contains(this); private set {} }
	public bool IsContraband { get => GameData.Instance.GetBanned().Contraband.Contains(this); private set {} }
	public bool IsOverpowered { get => GameData.Instance.GetBanned().Overpowered.Contains(this); private set {} }
	
	public bool HasBrewingRecipe { get => GameData.Instance.GetBrewingRecipe(this)!=null; private set {} }
	
	public bool HasCraftingRecipe { get => GameData.Instance.GetCraftingRecipe(this)!=null; private set {} }
	
	public bool IsFood { get => GameData.Instance.GetFood(this)!=null; private set {} }
	public int Energy { get => GameData.Instance.GetFood(this)?.Energy ?? 0; private set {} }
	public int Health { get => GameData.Instance.GetFood(this)?.Health ?? 0; private set {} }
	
	public bool Exists { get => GameData.Instance.GetItem(this)!=null; private set {} }
	public int StartingAmount { get => GameData.Instance.GetItem(this)?.StartingAmount ?? 0; private set {} }
	public List<GameCategory> Categories { get => GameData.Instance.GetItem(this)?.Categories ?? new List<GameCategory>(); private set {} }
	public int StartingPrice { get => GameData.Instance.GetItem(this)?.Price ?? 0; private set {} }
	public int Price { get => GameData.Instance.GetItem(this)?.Price ?? 0; private set {} }
	
	public bool IsMachine { get => GameData.Instance.GetMachine(this)!=null; private set {} }
	public int Power { get => GameData.Instance.GetMachine(this)?.Power ?? 0; private set {} }
	public Dictionary<GameItem, int> Consumed { get => GameData.Instance.GetMachine(this)?.Consumed ?? new Dictionary<GameItem, int>(); private set {} }
	public Dictionary<GameItem, int> Extracted { get => GameData.Instance.GetMachine(this)?.Extracted ?? new Dictionary<GameItem, int>(); private set {} }
	public Dictionary<GameItem, int> Produced { get => GameData.Instance.GetMachine(this)?.Produced ?? new Dictionary<GameItem, int>(); private set {} }
	public int Duration { get => GameData.Instance.GetMachine(this)?.Duration ?? -1; private set {} }
	
	public bool IsOre { get => GameData.Instance.GetOre(this)!=null; private set {} }
	public int ReducedTier { get => GameData.Instance.GetOre(this)?.ReducedTier ?? -1; private set {} }
	
	public bool IsPickaxe { get => GameData.Instance.GetPickaxe(this)!=null; private set {} }
	public int DoubleOreChance { get => GameData.Instance.GetPickaxe(this)?.DoubleOreChance ?? -1; private set {} }
	
	public bool IsPlacing { get => GameData.Instance.GetPlacing(this)!=null; private set {} }
	public GameTile Tile { get => GameData.Instance.GetPlacing(this)?.Tile ?? GameTile.Get("NONE"); private set {} }
	
	public bool IsRod { get => GameData.Instance.GetRod(this)!=null; private set {} }
	public int BaitConsumptionChance { get => GameData.Instance.GetRod(this)?.BaitConsumptionChance ?? -1; private set {} }
	public int LineBreakChance { get => GameData.Instance.GetRod(this)?.LineBreakChance ?? -1; private set {} }
	
	public bool IsWeapon { get => GameData.Instance.GetWeapon(this)!=null; private set {} }
}
