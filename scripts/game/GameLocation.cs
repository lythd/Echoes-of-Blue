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
	
	public bool Exists { get => GameData.Instance.GetLocation(this) != null; private set {} }
	public string ConflictAtStart { get => GameData.Instance.GetLocation(this)?.ConflictAtStart ?? "foreign"; private set {} }
	public GameCountry ControllerAtStart { get => GameData.Instance.GetLocation(this)?.ControllerAtStart ?? GameCountry.Get("NONE"); private set {} }
	public bool Lava { get => GameData.Instance.GetLocation(this)?.Lava ?? false; private set {} }
	public bool Air { get => GameData.Instance.GetLocation(this)?.Air ?? false; private set {} }
	public int Sun { get => GameData.Instance.GetLocation(this)?.Sun ?? -1; private set {} }
	public int Wind { get => GameData.Instance.GetLocation(this)?.Wind ?? -1; private set {} }
	public Dictionary<GameItem, List<Range>> Resources { get => GameData.Instance.GetLocation(this)?.Resources ?? new Dictionary<GameItem, List<Range>>(); private set {} }
	public Dictionary<string, List<GameMob>> Fighting { get => GameData.Instance.GetLocation(this)?.Fighting ?? new Dictionary<string, List<GameMob>>(); private set {} }
	public Dictionary<string, Dictionary<GameItem, Range>> Fishing { get => GameData.Instance.GetLocation(this)?.Fishing ?? new Dictionary<string, Dictionary<GameItem, Range>>(); private set {} }
	public Dictionary<string, Dictionary<GameItem, Range>> Mining { get => GameData.Instance.GetLocation(this)?.Mining ?? new Dictionary<string, Dictionary<GameItem, Range>>(); private set {} }
}
