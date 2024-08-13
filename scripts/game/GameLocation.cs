using System.Collections.Generic;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;
using Range = EchoesofBlue.scripts.serialization.Range;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameLocation>))]
public class GameLocation : GameEntity
{
	private GameLocation(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameLocation> Instances = new();
	
	public override string Name {
		get => TranslationServer.Translate($"{Id}_LOCATION_NAME");
		protected set {}
	}
	
	public override string Desc {
		get => TranslationServer.Translate($"{Id}_LOCATION_DESC");
		protected set {}
	}
	
	public static GameLocation Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameLocation(id);
		Instances[id] = item;
		return item;
	}
	
	public bool Exists => GameData.Instance.GetLocation(this) != null;
	public string ConflictAtStart => GameData.Instance.GetLocation(this)?.ConflictAtStart ?? "foreign";
	public GameCountry ControllerAtStart => GameData.Instance.GetLocation(this)?.ControllerAtStart ?? GameCountry.Get("NONE");
	public bool Lava => GameData.Instance.GetLocation(this)?.Lava ?? false;
	public bool Air => GameData.Instance.GetLocation(this)?.Air ?? false;
	public int Sun => GameData.Instance.GetLocation(this)?.Sun ?? -1;
	public int Wind => GameData.Instance.GetLocation(this)?.Wind ?? -1;
	public Dictionary<GameItem, List<Range>> Resources => GameData.Instance.GetLocation(this)?.Resources ?? new Dictionary<GameItem, List<Range>>();
	public Dictionary<string, List<GameMob>> Fighting => GameData.Instance.GetLocation(this)?.Fighting ?? new Dictionary<string, List<GameMob>>();
	public Dictionary<string, Dictionary<GameItem, Range>> Fishing => GameData.Instance.GetLocation(this)?.Fishing ?? new Dictionary<string, Dictionary<GameItem, Range>>();
	public Dictionary<string, Dictionary<GameItem, Range>> Mining => GameData.Instance.GetLocation(this)?.Mining ?? new Dictionary<string, Dictionary<GameItem, Range>>();
}