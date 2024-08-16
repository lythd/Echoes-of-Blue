using System.Collections.Generic;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameEffect>))]
public class GameEffect : GameEntity
{
	private GameEffect(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameEffect> Instances = new();
	
	public override string Name => TranslationServer.Translate($"{Id}_EFFECT_NAME");
	public override string Desc => TranslationServer.Translate($"{Id}_EFFECT_DESC");
	
	public static GameEffect Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameEffect(id);
		Instances[id] = item;
		return item;
	}
	
	public bool Exists => GameData.Instance.HasEffect(this);
}