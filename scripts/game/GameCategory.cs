using System.Collections.Generic;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameCategory>))]
public class GameCategory : GameEntity
{
	private GameCategory(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameCategory> Instances = new();
	
	public override string Name => TranslationServer.Translate($"{Id}_CATEGORY_NAME");
	public override string Desc => TranslationServer.Translate($"{Id}_CATEGORY_DESC");
	
	public static GameCategory Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameCategory(id);
		Instances[id] = item;
		return item;
	}
	
	public bool Exists => GameData.Instance.GetCategory(this) != null;
	public bool Show => GameData.Instance.GetCategory(this)?.Show ?? false;
}