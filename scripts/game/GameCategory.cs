using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameCategory>))]
public class GameCategory : GameEntity
{
	private GameCategory(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameCategory> _instances = new Dictionary<string, GameCategory>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_CATEGORY_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_CATEGORY_DESC"); }
		
		protected set {}
	}
	
	public static GameCategory Get(string id) {
		if (!_instances.TryGetValue(id, out GameCategory item))
		{
			item = new GameCategory(id);
			_instances[id] = item;
		}

		return item;
	}
}
