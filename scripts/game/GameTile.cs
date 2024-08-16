using System.Collections.Generic;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameTile>))]
public class GameTile : GameEntity
{
	private GameTile(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameTile> Instances = new();
	
	public override string Name => TranslationServer.Translate($"{Id}_TILE_NAME");
	public override string Desc => TranslationServer.Translate($"{Id}_TILE_DESC");
	
	public static GameTile Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameTile(id);
		Instances[id] = item;
		return item;
	}
	
	public bool Exists => GameData.Instance.GetTile(this) != null;
	public GameItem Drop => GameData.Instance.GetTile(this)?.Drop ?? GameItem.Get("NONE");
	public int Count => GameData.Instance.GetTile(this)?.Count ?? 0;
}