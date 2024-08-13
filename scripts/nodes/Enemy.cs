using System.Collections.Generic;
using System.Linq;
using EchoesofBlue.scripts.multiplayer;
using EchoesofBlue.scripts.stuff;
using Godot;

namespace EchoesofBlue.scripts;

public partial class Enemy : CharacterBody2D, IDamageableEntity
{
	[Signal]
	public delegate void DoAttackEventHandler(Enemy source, long id, bool flip, int damage, Vector2 pos);
	
	[Export]
	public long EnemyId { get; set; }
	
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
			if(_health <= 0) MarkDead();
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
	public int Damage { get; set; } = 5;
	
	public Vector2 Pos => Position;
	public Vector2 Direction { get; set; }
	public bool Flip
	{
		get => _sprite.Scale.X < 0;
		set => _sprite.Scale = new Vector2(value ? -0.1f : 0.1f, 0.1f);
	}
	
	private Attack _attack;
	private Sprite2D _sprite;
	private TextureProgressBar _healthBar;
	private List<IDamageableEntity> _sightList; // note that the sight list will also contain everything on the detect list
	private List<IDamageableEntity> _detectList;

	private IDamageableEntity Target => _sightList.Count == 0 ? null : _sightList.MinBy(e => e.SquaredDist(this));
	
	public const float Speed = 30.0f;

	public bool IsMultiplayer => MultiplayerManager.Instance.MultiplayerModeEnabled;
	public bool IsHost => GetMultiplayerAuthority() == Multiplayer.GetUniqueId() || !MultiplayerManager.Instance.MultiplayerModeEnabled;
	
	public void GetShit() {
		_healthBar = GetNode<TextureProgressBar>("HealthBar");
		_sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public override void _Ready()
	{
		GetShit();
		
		GetNodeOrNull<Node2D>("/root/Game")?.Call("connect_enemy_signals", this);
		
		if(!IsHost) {
			SetProcess(false);
			return;
		}

		_sightList = [];
		_detectList = [];
		
		MaxHealth = StartMaxHealth;
		Health = StartMaxHealth;
	}

	public override void _PhysicsProcess(double delta)
	{
		if(Direction.X > 0) Flip = false;
		else if(Direction.X < 0) Flip = true;
		if(!IsHost) return;
		Direction = Target == null ? Vector2.Zero : (Target.Pos - Position).Normalized();
		Velocity = Direction * Speed;
		if (Target != null && _detectList.Contains(Target)) Attack();
		MoveAndSlide();
	}

	public void Attack()
	{
		if (Attacking) return;
		if(IsHost) EmitSignal(SignalName.DoAttack, this, EnemyId, Flip, Damage, Position);
	}
	
	public void TakeAttack(Attack attack)
	{
		_attack = attack;
	}
	
	public void ClearAttack()
	{
		_attack = null;
	}

	public void MarkDead()
	{
		_attack.QueueFree();
		QueueFree();
	}
    
	private void _on_detect_area_body_entered(Node2D body)
	{
		if (!IsHost || body == this || body is not IDamageableEntity e) return;

		_detectList.Add(e);
	}
	
	private void _on_detect_area_body_exited(Node2D body)
	{
		if (!IsHost || body == this || body is not IDamageableEntity e) return;

		_detectList.Remove(e);
	}


	private void _on_sight_area_body_entered(Node2D body)
	{
		if (!IsHost || body == this || body is not IDamageableEntity e) return;
		
		_sightList.Add(e);
	}


	private void _on_sight_area_body_exited(Node2D body)
	{
		if (!IsHost || body == this || body is not IDamageableEntity e) return;

		_sightList.Remove(e);
	}
}
