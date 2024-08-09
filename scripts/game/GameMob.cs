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
	
	public bool Exists { get => GameData.Instance.GetMob(this) != null; private set {} }
	public string Type { get => GameData.Instance.GetMob(this)?.Type ?? "ENEMY"; private set {} }
	public int PiercingDamage { get => GameData.Instance.GetMob(this)?.PiercingDamage ?? -1; private set {} }
	public int BluntDamage { get => GameData.Instance.GetMob(this)?.BluntDamage ?? -1; private set {} }
	public int MaximumHealth { get => GameData.Instance.GetMob(this)?.MaximumHealth ?? -1; private set {} }
	public int Defense { get => GameData.Instance.GetMob(this)?.Defense ?? -1; private set {} }
	public int Speed { get => GameData.Instance.GetMob(this)?.Speed ?? -1; private set {} }
	public List<GameEffect> Effects { get => GameData.Instance.GetMob(this)?.Effects ?? new List<GameEffect>(); private set {} }
	public Dictionary<string, Dictionary<GameItem, Range>> Drops { get => GameData.Instance.GetMob(this)?.Drops ?? new Dictionary<string, Dictionary<GameItem, Range>>(); private set {} }
}
