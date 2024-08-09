using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameCountry>))]
public class GameCountry : GameEntity
{
	private GameCountry(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameCountry> _instances = new Dictionary<string, GameCountry>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_COUNTRY_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_COUNTRY_DESC"); }
		
		protected set {}
	}
	
	public static GameCountry Get(string id) {
		if (!_instances.TryGetValue(id, out GameCountry item))
		{
			item = new GameCountry(id);
			_instances[id] = item;
		}

		return item;
	}
	
	public bool Exists { get => GameData.Instance.HasCountry(this); private set {} }
}
