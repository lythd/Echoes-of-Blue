using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameMob>))]
public class GameMob : GameEntity
{
	private GameMob(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameMob> _instances = new Dictionary<string, GameMob>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_MOB_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_MOB_DESC"); }
		
		protected set {}
	}
	
	public static GameMob Get(string id) {
		if (!_instances.TryGetValue(id, out GameMob item))
		{
			item = new GameMob(id);
			_instances[id] = item;
		}

		return item;
	}
}
