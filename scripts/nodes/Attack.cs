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
	public int Id { get; set; }
	
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
		if(!IsHost)
		{
			SetProcess(false);
			SetPhysicsProcess(false);
			return;
		}
		Source.TakeAttack(this);
	}
	
	public override void _Process(double delta)
	{
		Position = Source.Pos;
		Flip = Source.Flip;
	}
	
	/*public void ReceiveSource(IDamageableEntity source)
	{
		Source = source;
	}*/
	
	// TODO : remove these once I get rid of the gdscript code and use the above, gdscript just hates idamgeableentity idk
	public void ReceiveSource(Player source)
	{
		Source = source;
	}
	
	public void ReceiveSourceEnemy(Enemy source)
	{
		Source = source;
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (!IsHost) return;
		if (body is not IDamageableEntity e || e == Source) return;
		e.Health -= Damage;
	}
	
	private void _on_timer_timeout()
	{
		if(!IsHost) return;
		Source?.ClearAttack();
		QueueFree();
	}
}
