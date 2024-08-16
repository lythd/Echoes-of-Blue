using EchoesofBlue.scripts.game;
using Godot;

namespace EchoesofBlue.scripts;

public partial class InputSynchronizer : MultiplayerSynchronizer
{
	private Player _player;
	
	public Godot.Vector2 InputDirection;
	public string Username;

	public override void _Ready()
	{
		_player = GetNode<CharacterBody2D>("..") as Player;
		if(GetMultiplayerAuthority() != Multiplayer.GetUniqueId())
		{
			SetProcess(false);
			SetPhysicsProcess(false);
		}

		InputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Username = multiplayer.steam.SteamManager.Instance.SteamUsername;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		InputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("sneak")) Rpc(nameof(Sneak));
		if(Input.IsActionJustPressed("attack")) Rpc(nameof(Attack));
		if(Input.IsActionJustPressed("cry")) Rpc(nameof(Cry));
		if(Input.IsActionJustPressed("angry")) Rpc(nameof(Angry));
		if(Input.IsActionJustPressed("shock")) Rpc(nameof(Shock));
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void Sneak()
	{
		if(Multiplayer.IsServer()) _player.PressSneak();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void Attack()
	{
		if(Multiplayer.IsServer()) _player.Attack();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void Cry()
	{
		if(Multiplayer.IsServer()) _player.Cry();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void Angry()
	{
		if(Multiplayer.IsServer()) _player.Angry();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void Shock()
	{
		if(Multiplayer.IsServer()) _player.Shock();
	}

	public void SetMapOpenRpc(bool val)
	{
		Rpc(nameof(SetMapOpen), val);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void SetMapOpen(bool val)
	{
		if(Multiplayer.IsServer()) _player.MapOpen = val;
	}

	public void CallCharacterRpc(Color color, string part)
	{
		Rpc(nameof(CallCharacter), color, part);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void CallCharacter(Color color, string part)
	{
		if(Multiplayer.IsServer()) _player.CallCharacter(color, part);
	}

	public void SyncSteamRpc(string steamId, string steamUsn)
	{
		Rpc(nameof(SyncSteam), steamId, steamUsn);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void SyncSteam(string steamId, string steamUsn)
	{
		if(Multiplayer.IsServer()) _player.SyncSteam(steamId, steamUsn);
	}
	
	public void SetPlayerLocationRpc(GameLocation location) => Rpc(nameof(SetPlayerLocation), location.ToString());
	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void SetPlayerLocation(string location)
	{
		if(Multiplayer.IsServer()) GameData.Instance.SetPlayerLocation(_player.SteamId, location);
	}

	public void SetPlayerNameRpc(string name) => Rpc(nameof(SetPlayerName), name); // TODO : Will need some server side authentication with these rpcs, to make sure they are reasonable data

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void SetPlayerName(string name)
	{
		if(Multiplayer.IsServer()) GameData.Instance.SetPlayerName(_player.SteamId, name);
	}
}