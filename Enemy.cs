using Godot;
using System;

public partial class Enemy : CharacterBody2D, IDamageableEntity
{
	private TextureProgressBar _healthBar;
	
	[Export]
	public int StartMaxHealth = 20;

	[Export]
	public int Health { get; set; }
	
	private int _maxHealth;
	
	[Export]
	public int MaxHealth
	{
		get => _maxHealth;
		set
		{
			_maxHealth = value;
			_healthBar.MinValue = 3.0f * _maxHealth/(6.0f - _healthBar.Size.X);
			_healthBar.MaxValue = _maxHealth - _healthBar.MinValue;
			if(Health > _maxHealth) Health = _maxHealth;
		}
	}

	[Export]
	public int Damage { get; set; } = 20;
	
	public const float Speed = 30.0f;

	public override void _Ready()
	{
		if(GetMultiplayerAuthority() != Multiplayer.GetUniqueId() && MultiplayerManager.Instance.MultiplayerModeEnabled) {
			SetProcess(false);
			SetPhysicsProcess(false);
			return;
		}
		MaxHealth = StartMaxHealth;
		Health = StartMaxHealth;
		_healthBar = GetNode<TextureProgressBar>("HealthBar");
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = Vector2.Up * Speed;
		MoveAndSlide();
	}
}
