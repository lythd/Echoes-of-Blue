using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameTile>))]
public class GameTile : GameEntity
{
	private GameTile(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameTile> _instances = new Dictionary<string, GameTile>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_TILE_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_TILE_DESC"); }
		
		protected set {}
	}
	
	public static GameTile Get(string id) {
		if (!_instances.TryGetValue(id, out GameTile item))
		{
			item = new GameTile(id);
			_instances[id] = item;
		}

		return item;
	}
}
