using Godot;
using System;

public partial class Attack : AnimatedSprite2D
{
	
	private Sprite2D _sprite;
	
	private Timer _timer;
	
	[Export]
	public int Damage { get; set; }
	
	[Export]
	public CharacterBody2D Source { get; set; }
	
	[Export]
	public bool Flip
	{
		get => _sprite.Scale.X == -1;
		set => _sprite.Scale = new Vector2(value ? 1 : -1, 1);
	}
	
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
		_timer = GetNode<Timer>("Timer");
		_timer.Start();
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is IDamageableEntity e && e != Source) e.Health -= Damage;
	}
	
	private void _on_timer_timeout()
	{
		QueueFree();
	}
}
