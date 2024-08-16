using System.Collections.Generic;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;
using Range = EchoesofBlue.scripts.serialization.Range;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameMob>))]
public class GameMob : GameEntity
{
	private GameMob(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameMob> Instances = new();
	
	public override string Name => TranslationServer.Translate($"{Id}_MOB_NAME");
	public override string Desc => TranslationServer.Translate($"{Id}_MOB_DESC");
	
	public static GameMob Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameMob(id);
		Instances[id] = item;
		return item;
	}
	
	public bool Exists => GameData.Instance.GetMob(this) != null;
	public string Type => GameData.Instance.GetMob(this)?.Type ?? "ENEMY";
	public int PiercingDamage => GameData.Instance.GetMob(this)?.PiercingDamage ?? -1;
	public int BluntDamage => GameData.Instance.GetMob(this)?.BluntDamage ?? -1;
	public int MaximumHealth => GameData.Instance.GetMob(this)?.MaximumHealth ?? -1;
	public int Defense => GameData.Instance.GetMob(this)?.Defense ?? -1;
	public int Speed => GameData.Instance.GetMob(this)?.Speed ?? -1;
	public List<GameEffect> Effects => GameData.Instance.GetMob(this)?.Effects ?? [];
	public LootTableRange<GameItem> Drops => GameData.Instance.GetMob(this)?.Drops ?? new();
}