using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameEffect>))]
public class GameEffect : GameEntity
{
	private GameEffect(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameEffect> _instances = new Dictionary<string, GameEffect>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_EFFECT_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_EFFECT_DESC"); }
		
		protected set {}
	}
	
	public static GameEffect Get(string id) {
		if (!_instances.TryGetValue(id, out GameEffect item))
		{
			item = new GameEffect(id);
			_instances[id] = item;
		}

		return item;
	}
	
	public bool Exists { get => GameData.Instance.HasEffect(this); private set {} }
}
