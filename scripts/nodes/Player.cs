using System;
using EchoesofBlue.scripts.multiplayer;
using EchoesofBlue.scripts.stuff;
using Godot;

namespace EchoesofBlue.scripts;

public partial class Player : CharacterBody2D, IDamageableEntity
{
	
	// TODO : Make IPlayerServer in here, and a PlayerMpServer and PlayerSpServer so I can divy up the code to be more organized. Separation would help code flow. Do similar for attack and enemy.
	
	// SIGNALS //
	[Signal]
	public delegate void ShowAllEventHandler(bool showAll);
	[Signal]
	public delegate void SyncCharacterEventHandler(Player player);
	[Signal]
	public delegate void AddMapEventHandler(Map map);
	[Signal]
	public delegate void CheckTilePropertiesEventHandler(Vector2 globalPositionPlayer, Player player);
	[Signal]
	public delegate void DoAttackEventHandler(Player source, long id, bool flip, int damage, Vector2 pos);
	
	
	// SCENES //
	private PackedScene _mapScene;
	
	
	// NODES //
	private Map _map;
	private Character _character;
	private TextureProgressBar _healthBar;
	private Label _usernameLabel;
	private Camera2D _camera;
	private CollisionShape2D _collision;
	private Timer _respawnTimer;
	public InputSynchronizer InputSynchronizer { get; private set; }
	
	
	// PROPERTIES/FIELDS //
	public const double Speed = 75.0;
	
	public bool Attacking => _attack != null;
	
	[Export] public Vector2 Direction { get; set; }
	
	[Export] public string Username
	{
		get => _usernameLabel?.Text ?? "";
		set { if(_usernameLabel!=null) _usernameLabel.Text = value; }
	}
	[Export] public bool Sneaking;
	[Export] public bool Crying;
	[Export] public bool Angried;
	[Export] public bool Shocked;
	[Export] public bool MapOpen;
	[Export] public bool Alive = true;
	[Export] public bool TileIsSlippery = false;

	private long _playerId;
	[Export] public long PlayerId
	{
		get => _playerId;
		set
		{
			_playerId = value;
			(InputSynchronizer ?? GetNode<MultiplayerSynchronizer>("InputSynchronizer") as InputSynchronizer)?.SetMultiplayerAuthority(Convert.ToInt32(_playerId));
		}
	}
	
	[Export] public int StartMaxHealth = 20;

	private int _health;

	[Export] public int Health
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
	
	[Export] public int MaxHealth
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

	[Export] public int Damage { get; set; } = 5;
	
	public int AttackOffset { get; set; } = 4;
	
	public Vector2 Pos => Position;
	public bool Flip
	{
		get => _character.Scale.X < 0;
		set => _character.Scale = new Vector2(value ? -1 : 1, 1);
	}
	
	[Export] public float KbResistance { get; set; } = 1.5f;
	[Export] public float KbStrength { get; set; } = 30f;
	public Vector2 Kb { get; set; } = Vector2.Zero;
	
	private Attack _attack;
	
	public bool IsMultiplayer => MultiplayerManager.Instance.MultiplayerModeEnabled;
	public bool IsOwner => Multiplayer.GetUniqueId() == PlayerId || !MultiplayerManager.Instance.MultiplayerModeEnabled;
	public bool IsHost => GetMultiplayerAuthority() == Multiplayer.GetUniqueId() || !MultiplayerManager.Instance.MultiplayerModeEnabled;
	
	// GODOT METHODS //

	private void GetShit()
	{
		_mapScene = GD.Load<PackedScene>("res://scenes/map.tscn");
		_character = GetNode<Node2D>("Character") as Character;
		_healthBar = GetNode<TextureProgressBar>("HealthBar");
		_camera = GetNode<Camera2D>("Camera2D");
		_usernameLabel = GetNode<Label>("Username");
		_collision = GetNode<CollisionShape2D>("CollisionShape2D");
		_respawnTimer = GetNode<Timer>("RespawnTimer");
		InputSynchronizer = GetNode<MultiplayerSynchronizer>("InputSynchronizer") as InputSynchronizer;
	}
	
	public override void _Ready()
	{
		GetShit();
		
		if(IsHost) {
			MaxHealth = StartMaxHealth;
			Health = StartMaxHealth;
		}
		
		GetNodeOrNull<Node2D>("/root/Game")?.Call("connect_player_signals", this);

		if(IsOwner) EmitSignal(SignalName.SyncCharacter, this);
		
		SetCamera();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!Alive) return;
		
		if(IsOwner) EmitSignal(SignalName.CheckTileProperties, GlobalPosition, this);
		//if(IsHost && !Alive) SetAlive();
		ApplyMovementFromInput();
		ApplyAnimations();
		Kb = Kb.MoveToward(Vector2.Zero, KbResistance);
		Velocity += Kb;
		MoveAndSlide();

		if(_usernameLabel != null && GameData.Instance.PlayerName != "") Username = GameData.Instance.PlayerName;
		
		if(IsHost && IsOwner && Input.IsActionJustPressed("debug_spawn_enemy"))
			GetNodeOrNull<Node2D>("/root/Game")?.Call("spawn_enemy", Position + new Random().NextVector()*250);
	}
	
	
	
	
	
	
	
	
	
	
	// MY METHODS //
	
	public void SetPlayerId(long id)
	{
		PlayerId = id;
	}
	
	public void SetCamera() {
		if(IsOwner)
		{
			_camera.MakeCurrent();
			_camera.Enabled = true;
		}
		else _camera.Enabled = false;
	}
	
	public void PrintNodeTree(Node rootNode = null, int indent = 0)
	{
		if(rootNode == null) rootNode = GetTree().Root;
		
		string indentStr = "";
		for(int i = 0; i < indent; i++) indentStr += "  ";
		
		GD.Print($"{indentStr}|-- {rootNode.Name}");
		
		foreach(var child in rootNode.GetChildren()) PrintNodeTree(child, indent+1);
	}
	
	// TODO : use an animation tree? that way can have transitions, like the walk animation as a transition before running, and then it also not going to full speed until you run? also so emotes would go away after a certain amt of time and stuff
	public void ApplyAnimations()
	{
		if(Direction.X > 0) Flip = false;
		else if(Direction.X < 0) Flip = true;
		if(Attacking) {}
		else if(Crying) _character.Play("Cry");
		else if(Angried) _character.Play("Angry");
		else if(Shocked) _character.Play("Shock");
		else if(Sneaking) _character.Play("Sneak");
		else if(Direction != Vector2.Zero) _character.Play("Run");
		else _character.Play("Idle");
	}

	public void ApplyMovementFromInput()
	{
		Direction = IsOwner ? Input.GetVector("move_left", "move_right", "move_up", "move_down") : InputSynchronizer.InputDirection;
		if(IsMultiplayer) GameData.Instance.PlayerName = InputSynchronizer.Username;
		if(MapOpen) Direction = Vector2.Zero;
		
		// client side controls
		if(IsOwner && Input.IsActionJustPressed("map")) PressMap();
		if(IsOwner && Input.IsActionJustPressed("debug_print_scene_tree")) PrintNodeTree();
		
		// singleplayer versions of host side controls
		if(!IsMultiplayer && Input.IsActionJustPressed("sneak")) PressSneak();
		if(!IsMultiplayer && Input.IsActionJustPressed("attack")) Attack();
		if(!IsMultiplayer && Input.IsActionJustPressed("cry")) Cry();
		if(!IsMultiplayer && Input.IsActionJustPressed("angry")) Angry();
		if(!IsMultiplayer && Input.IsActionJustPressed("shock")) Shock();
		
		var speed = Sneaking ? Speed/3.0f : Speed;
		if(Direction != Vector2.Zero)
		{
			Velocity = Direction * (float)speed;
			Crying = false; Angried = false; Shocked = false;
		}
		else if(TileIsSlippery) Velocity = new Vector2((float)Mathf.MoveToward(Velocity.X, 0, speed), (float)Mathf.MoveToward(Velocity.Y, 0, speed));
		else Velocity = Vector2.Zero;
	}

	public void PressSneak()
	{
		Crying = false; Angried = false; Shocked = false;
		if(MapOpen) return;
		Sneaking = !Sneaking;
	}

	public void Attack()
	{
		if(Attacking) return;
		_character.Play("Attack");
		Crying = false; Angried = false; Shocked = false;
		if(IsHost) EmitSignal(SignalName.DoAttack, this, PlayerId, Flip, Damage, Position);
	}
	
	public void TakeAttack(Attack attack)
	{
		_attack = attack;
	}
	
	public void ClearAttack()
	{
		_attack = null;
	}

	public void Cry()
	{
		Angried = false; Shocked = false;
		Crying = !Crying;
	}

	public void Angry()
	{
		Crying = false; Shocked = false;
		Angried = !Angried;
	}

	public void Shock()
	{
		Crying = false; Angried = false;
		Shocked = !Shocked;
	}

	public void PressMap()
	{
		Crying = false; Angried = false; Shocked = false;
		MapOpen = _map == null;
		InputSynchronizer.SetMapOpenRpc(MapOpen);
		if(MapOpen)
		{
			_map = _mapScene.Instantiate() as Map;
			_map!.Player = this;
			if(IsOwner) EmitSignal(SignalName.AddMap, _map);
			if(IsOwner) EmitSignal(SignalName.ShowAll, false);
		}
		else ComeBackFromMap();
	}

	public void ComeBackFromMap()
	{
		_map.QueueFree();
		_map = null;
		MapOpen = false;
		InputSynchronizer.SetMapOpenRpc(MapOpen);
		if(IsOwner) EmitSignal(SignalName.ShowAll, true);
		SetCamera();
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}

	public void MarkDead()
	{
		_attack?.QueueFree();
		_attack = null;
		Alive = false;
		_collision.SetDeferred("disabled", true);
		_respawnTimer.Start();
	}

	public void Respawn()
	{
		Position = MultiplayerManager.Instance.RespawnPoint;
		_collision.SetDeferred("disabled", false);
		Health = MaxHealth;
		Alive = true;
		Engine.TimeScale = 1.0;
	}

	public void CallCharacter(Color color, string part)
	{
		_character.Call("SetHSV", part, color.H, color.S, color.V);
	}
}
