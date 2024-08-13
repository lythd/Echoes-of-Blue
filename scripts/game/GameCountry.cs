using System.Collections.Generic;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameCountry>))]
public class GameCountry : GameEntity
{
	private GameCountry(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameCountry> Instances = new();
	
	public override string Name {
		get => TranslationServer.Translate($"{Id}_COUNTRY_NAME");
		protected set {}
	}
	
	public override string Desc {
		get => TranslationServer.Translate($"{Id}_COUNTRY_DESC");
		protected set {}
	}
	
	public static GameCountry Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameCountry(id);
		Instances[id] = item;
		return item;
	}
	
	public bool Exists => GameData.Instance.HasCountry(this);
}