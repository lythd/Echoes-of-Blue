using System.Collections.Generic;
using System.Linq;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameItem>))]
public class GameItem : GameEntity
{
	private GameItem(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameItem> Instances = new();
	
	public override string Name {
		get => TranslationServer.Translate($"{Id}_ITEM_NAME");
		protected set {}
	}
	
	public override string Desc {
		get => TranslationServer.Translate($"{Id}_ITEM_DESC");
		protected set {}
	}
	
	public static GameItem Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameItem(id);
		Instances[id] = item;
		return item;
	}
	
	public List<GameEffect> Effects => (GameData.Instance.GetAccessory(this)?.Effects ?? []).Concat(GameData.Instance.GetAmmo(this)?.Effects ??
		[]).Concat(GameData.Instance.GetArmor(this)?.Effects ??
		           []).Concat(GameData.Instance.GetWeapon(this)?.Effects ??
		                      []).ToList();

	public int Durability => GameData.Instance.GetAccessory(this)?.Durability ?? GameData.Instance.GetArmor(this)?.Durability ?? GameData.Instance.GetPickaxe(this)?.Durability ?? GameData.Instance.GetRod(this)?.Durability ?? GameData.Instance.GetWeapon(this)?.Durability ?? -1;
	public int BluntDamage => GameData.Instance.GetAmmo(this)?.BluntDamage ?? GameData.Instance.GetWeapon(this)?.BluntDamage ?? -1;
	public int PiercingDamage => GameData.Instance.GetAmmo(this)?.PiercingDamage ?? GameData.Instance.GetWeapon(this)?.PiercingDamage ?? -1;
	public int Priority => GameData.Instance.GetArmor(this)?.Priority ?? GameData.Instance.GetWeapon(this)?.Priority ?? 0;
	public int FishingPower => GameData.Instance.GetBait(this)?.FishingPower ?? GameData.Instance.GetRod(this)?.FishingPower ?? -1;
	public Dictionary<GameItem, int> Inputs => GameData.Instance.GetBrewingRecipe(this)?.Inputs ?? GameData.Instance.GetCraftingRecipe(this)?.Inputs ?? new Dictionary<GameItem, int>();
	public int Amount => GameData.Instance.GetBrewingRecipe(this)?.Amount ?? GameData.Instance.GetCraftingRecipe(this)?.Amount ?? 0;
	public int Tier => GameData.Instance.GetOre(this)?.Tier ?? GameData.Instance.GetPickaxe(this)?.Tier ?? -1;

	public bool IsAccessory => GameData.Instance.GetAccessory(this)!=null;

	public bool IsAmmo => GameData.Instance.GetAmmo(this)!=null;

	public bool IsArmor => GameData.Instance.GetArmor(this)!=null;
	public int Defense => GameData.Instance.GetArmor(this)?.Defense ?? -1;

	public bool IsBait => GameData.Instance.GetBait(this)!=null;
	public GameItem Drop => GameData.Instance.GetBait(this)?.Drop ?? Get("NONE");

	public bool IsGodlike => GameData.Instance.GetBanned().Godlike.Contains(this);
	public bool IsInappropriate => GameData.Instance.GetBanned().Inappropriate.Contains(this);
	public bool IsArmorChoice => GameData.Instance.GetBanned().ArmorChoices.Contains(this);
	public bool IsWeaponChoice => GameData.Instance.GetBanned().WeaponChoices.Contains(this);
	public bool IsContraband => GameData.Instance.GetBanned().Contraband.Contains(this);
	public bool IsOverpowered => GameData.Instance.GetBanned().Overpowered.Contains(this);

	public bool HasBrewingRecipe => GameData.Instance.GetBrewingRecipe(this)!=null;

	public bool HasCraftingRecipe => GameData.Instance.GetCraftingRecipe(this)!=null;

	public bool IsFood => GameData.Instance.GetFood(this)!=null;
	public int Energy => GameData.Instance.GetFood(this)?.Energy ?? 0;
	public int Health => GameData.Instance.GetFood(this)?.Health ?? 0;

	public bool Exists => GameData.Instance.GetItem(this)!=null;
	public int StartingAmount => GameData.Instance.GetItem(this)?.StartingAmount ?? 0;
	public List<GameCategory> Categories => GameData.Instance.GetItem(this)?.Categories ?? [];
	public int StartingPrice => GameData.Instance.GetItem(this)?.Price ?? 0;
	public int Price => GameData.Instance.GetItem(this)?.Price ?? 0;

	public bool IsMachine => GameData.Instance.GetMachine(this)!=null;
	public int Power => GameData.Instance.GetMachine(this)?.Power ?? 0;
	public Dictionary<GameItem, int> Consumed => GameData.Instance.GetMachine(this)?.Consumed ?? new Dictionary<GameItem, int>();
	public Dictionary<GameItem, int> Extracted => GameData.Instance.GetMachine(this)?.Extracted ?? new Dictionary<GameItem, int>();
	public Dictionary<GameItem, int> Produced => GameData.Instance.GetMachine(this)?.Produced ?? new Dictionary<GameItem, int>();
	public int Duration => GameData.Instance.GetMachine(this)?.Duration ?? -1;

	public bool IsOre => GameData.Instance.GetOre(this)!=null;
	public int ReducedTier => GameData.Instance.GetOre(this)?.ReducedTier ?? -1;

	public bool IsPickaxe => GameData.Instance.GetPickaxe(this)!=null;
	public int DoubleOreChance => GameData.Instance.GetPickaxe(this)?.DoubleOreChance ?? -1;

	public bool IsPlacing => GameData.Instance.GetPlacing(this)!=null;
	public GameTile Tile => GameData.Instance.GetPlacing(this)?.Tile ?? GameTile.Get("NONE");

	public bool IsRod => GameData.Instance.GetRod(this)!=null;
	public int BaitConsumptionChance => GameData.Instance.GetRod(this)?.BaitConsumptionChance ?? -1;
	public int LineBreakChance => GameData.Instance.GetRod(this)?.LineBreakChance ?? -1;

	public bool IsWeapon => GameData.Instance.GetWeapon(this)!=null;
}