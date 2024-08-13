using Godot;

namespace EchoesofBlue.scripts;

public partial class Enemy : CharacterBody2D, IDamageableEntity
{
	private TextureProgressBar _healthBar;
	
	public bool Attacking => _attack != null;
	
	[Export]
	public int StartMaxHealth = 20;

	private int _health;
	
	[Export]
	public int Health
	{
		get => _health;
		set
		{
			_health = value;
			if(_healthBar == null) return;
			_healthBar.Value = _health;
		}
	}
	
	private int _maxHealth;
	
	[Export]
	public int MaxHealth
	{
		get => _maxHealth;
		set
		{
			_maxHealth = value;
			if(_healthBar == null) return;
			_healthBar.MinValue = 3.0f * _maxHealth/(6.0f - _healthBar.Size.X);
			_healthBar.MaxValue = _maxHealth - _healthBar.MinValue;
			if(Health > _maxHealth) Health = _maxHealth;
		}
	}

	[Export]
	public int Damage { get; set; } = 20;
	
	public Vector2 Pos => Position;
	public bool Flip
	{
		get => _sprite.Scale.X < 0;
		set => _sprite.Scale = new Vector2(value ? -1 : 1, 1);
	}
	
	private Attack _attack = null;
	private Sprite2D _sprite = null;
	
	public const float Speed = 30.0f;

	public void GetShit() {
		_healthBar = GetNode<TextureProgressBar>("HealthBar");
		_sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public override void _Ready()
	{
		GetShit();
		
		if(GetMultiplayerAuthority() != Multiplayer.GetUniqueId() && multiplayer.MultiplayerManager.Instance.MultiplayerModeEnabled) {
			SetProcess(false);
			SetPhysicsProcess(false);
			return;
		}
		
		MaxHealth = StartMaxHealth;
		Health = StartMaxHealth;
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = Vector2.Up * Speed;
		MoveAndSlide();
	}
	
	
	
	public void TakeAttack(Attack attack)
	{
		_attack = attack;
	}
	
	public void ClearAttack()
	{
		_attack = null;
	}
}