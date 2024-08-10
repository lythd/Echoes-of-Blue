using Godot;
using System;

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
		Username = SteamManager.Instance.SteamUsername;
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

	public void Sneak()
	{
		if(Multiplayer.IsServer()) _player.PressSneak();
	}

	public void Attack()
	{
		if(Multiplayer.IsServer()) _player.Attack();
	}

	public void Cry()
	{
		if(Multiplayer.IsServer()) _player.Cry();
	}

	public void Angry()
	{
		if(Multiplayer.IsServer()) _player.Angry();
	}

	public void Shock()
	{
		if(Multiplayer.IsServer()) _player.Shock();
	}

	public void SetMapOpenRpc(bool val)
	{
		Rpc(nameof(SetMapOpen), val);
	}

	public void SetMapOpen(bool val)
	{
		if(Multiplayer.IsServer()) _player.MapOpen = val;
	}

	public void CallCharacterRpc(Color color, string part)
	{
		Rpc(nameof(CallCharacter), color, part);
	}

	public void CallCharacter(Color color, string part)
	{
		if(Multiplayer.IsServer()) _player.CallCharacter(color, part);
	}
}
