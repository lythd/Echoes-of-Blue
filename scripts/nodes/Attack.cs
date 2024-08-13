using System;
using Godot;

namespace EchoesofBlue.scripts;

public partial class Attack : Node2D
{
	
	[Signal]
	public delegate void SyncSourceEventHandler(Attack attack);
	
	[Export]
	public int Damage { get; set; }
	
	[Export]
	public int PlayerId { get; set; }
	
	public IDamageableEntity Source { get; set; }
	
	public bool IsHost => GetMultiplayerAuthority() == Multiplayer.GetUniqueId() || !multiplayer.MultiplayerManager.Instance.MultiplayerModeEnabled;
	
	[Export]
	public bool Flip
	{
		get => Scale.X < 0;
		set => Scale = new Vector2(value ? -1 : 1, 1);
	}
	
	public override void _Ready()
	{
		GD.Print($" > ready {PlayerId} {Multiplayer.GetUniqueId()}");
		if(!IsHost)
		{
			SetProcess(false);
			SetPhysicsProcess(false);
			return;
		}
		GD.Print(" > processing");
		Source.TakeAttack(this);
	}
	
	public override void _Process(double delta)
	{
		Position = Source.Pos;
		Flip = Source.Flip;
	}
	
	public void ReceiveSource(Player source)
	{
		if(source is IDamageableEntity e) Source = e;
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
		GD.Print(" > body entered");
		if (body is not IDamageableEntity e || e == Source) return;
		e.Health -= Damage;
		GD.Print($" > Damaging entity by {Damage}, now at {e.Health} health!");
	}
	
	private void _on_timer_timeout()
	{
		if(!IsHost) return;
		Source?.ClearAttack();
		QueueFree();
	}
}
