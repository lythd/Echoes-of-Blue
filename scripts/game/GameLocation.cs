using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameLocation>))]
public class GameLocation : GameEntity
{
	private GameLocation(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameLocation> _instances = new Dictionary<string, GameLocation>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_LOCATION_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_LOCATION_DESC"); }
		
		protected set {}
	}
	
	public static GameLocation Get(string id) {
		if (!_instances.TryGetValue(id, out GameLocation item))
		{
			item = new GameLocation(id);
			_instances[id] = item;
		}

		return item;
	}
}
