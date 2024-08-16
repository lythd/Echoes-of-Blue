using System;
using System.Collections.Generic;
using System.Numerics;
using EchoesofBlue.scripts.serialization;
using Godot;
using Newtonsoft.Json;
using Vector2 = Godot.Vector2;

namespace EchoesofBlue.scripts.game;

[JsonConverter(typeof(GameConverter<GameUser>))]
public class GameUser : GameEntity
{
	private GameUser(string id) {
		Id = id;
	}
	
	private static readonly Dictionary<string, GameUser> Instances = new();

	public override string Name => "";
	public override string Desc => "";
	
	public static GameUser Get(string id) {
		if (Instances.TryGetValue(id, out var item)) return item;
		item = new GameUser(id);
		Instances[id] = item;
		return item;
	}

	private User User => GameData.Instance.GetUser(this);
	public bool Exists => User != null;
	public string PlayerName
	{
		get => User?.Name ?? "";
		set => User.Name = value;
	}
	public Dictionary<GameItem, BigInteger> Bag
	{
		get => User?.Bag ?? new();
		set => User.Bag = value;
	}
	public Equipment Equipment
	{
		get => User?.Equipment ?? new();
		set => User.Equipment = value;
	}
	public BigInteger Money
	{
		get => User?.Money ?? 0;
		set => User.Money = value;
	}
	public BigInteger Energy
	{
		get => User?.Energy ?? 0;
		set => User.Energy = value;
	}
	public string Gang
	{
		get => User?.Gang ?? "NONE";
		set => User.Gang = value;
	}
	public GameLocation Location
	{
		get => User?.Location ?? GameLocation.Get("NONE");
		set => User.Location = value;
	}
	public LocationArea Area
	{
		get => User?.Area ?? LocationArea.Main;
		set => User.Area = value;
	}
	public Vector2 Position
	{
		get => User?.Position.V ?? Vector2.Zero;
		set => User.Position.V = value;
	}
	public int Wins
	{
		get => User?.Wins ?? 0;
		set => User.Wins = value;
	}
	public int Losses
	{
		get => User?.Losses ?? 0;
		set => User.Losses = value;
	}
	public Dictionary<GameMob, int> MobKills
	{
		get => User?.MobKills ?? new();
		set => User.MobKills = value;
	}
	public Dictionary<GameMob, int> MobKos
	{
		get => User?.MobKos ?? new();
		set => User.MobKos = value;
	}
	public Dictionary<GameUser, int> Victories
	{
		get => User?.Victories ?? new();
		set => User.Victories = value;
	}
	public Dictionary<GameLocation, Plot> Plots
	{
		get => User?.Plots ?? new();
		set => User.Plots = value;
	}
	public List<Process> Processes
	{
		get => User?.Processes ?? [];
		set => User.Processes = value;
	}
	public bool PumpkinSeed
	{
		get => User?.PumpkinSeed ?? false;
		set => User.PumpkinSeed = value;
	}
	public bool RocketAcquired
	{
		get => User?.RocketAcquired ?? false;
		set => User.RocketAcquired = value;
	}
	public Dictionary<string, int> Quests
	{
		get => User?.Quests ?? new();
		set => User.Quests = value;
	}
	public List<string> Boycotts
	{
		get => User?.Boycotts ?? [];
		set => User.Boycotts = value;
	}
	public Dictionary<string, Stall> Stalls
	{
		get => User?.Stalls ?? new();
		set => User.Stalls = value;
	}
	public DateTime LastBeg
	{
		get => User?.LastBeg ?? DateTime.UnixEpoch;
		set => User.LastBeg = value;
	}
	public Dictionary<GameUser, Tuple<int, int>> Loans
	{
		get => User?.Loans ?? new();
		set => User.Loans = value;
	}

	public float Health
	{
		get => User?.Health ?? 20.0f;
		set => User.Health = value;
	}
}