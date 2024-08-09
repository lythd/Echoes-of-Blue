using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonConverter(typeof(GameConverter<GameMachine>))]
public class GameMachine : GameEntity
{
	private GameMachine(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameMachine> _instances = new Dictionary<string, GameMachine>();
	
	public override string Name {
		get { return TranslationServer.Translate($"{Id}_MACHINE_NAME"); }
		
		protected set {}
	}
	
	public override string Desc {
		get { return TranslationServer.Translate($"{Id}_MACHINE_DESC"); }
		
		protected set {}
	}
	
	public static GameMachine Get(string id) {
		if (!_instances.TryGetValue(id, out GameMachine item))
		{
			item = new GameMachine(id);
			_instances[id] = item;
		}

		return item;
	}
}
