using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameItem>))]
public class GameItem : GameEntity
{
	private GameItem(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameItem> _instances = new Dictionary<string, GameItem>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_ITEM_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_ITEM_DESC"); }
		
		protected set {}
	}
	
	public static GameItem Get(string id) {
		if (!_instances.TryGetValue(id, out GameItem item))
		{
			item = new GameItem(id);
			_instances[id] = item;
		}

		return item;
	}
}
