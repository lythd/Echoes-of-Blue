using Godot;
using System;

public partial class Player : CharacterBody2D, IDamageableEntity
{
	
	// SIGNALS //
	[Signal]
	public delegate void ShowAllEventHandler(bool showAll);
	[Signal]
	public delegate void SyncCharacterEventHandler(Player player);
	[Signal]
	public delegate void AddMapEventHandler(Map map);
	[Signal]
	public delegate void CheckTilePropertiesEventHandler(Godot.Vector2 globalPositionPlayer, Player player);
	
	
	// SCENES //
	private PackedScene _mapScene;
	
	
	// NODES //
	private Map _map = null;
	private Character _character;
	private TextureProgressBar _healthBar;
	private Label _usernameLabel;
	private Camera2D _camera;
	private CollisionShape2D _collision;
	private Timer _respawnTimer;
	public InputSynchronizer InputSynchronizer { get; private set; }
	
	
	// PROPERTIES/FIELDS //
	public const double SPEED = 75.0;
	
	[Export]
	public Godot.Vector2 Direction { get; set; }
	
	[Export]
	public string Username
	{
		get => _usernameLabel?.Text ?? "";
		set { if(_usernameLabel!=null) _usernameLabel.Text = value; }
	}
	[Export]
	public bool Sneaking = false;
	[Export]
	public bool Attacking = false;
	[Export]
	public bool Crying = false;
	[Export]
	public bool Angried = false;
	[Export]
	public bool Shocked = false;
	[Export]
	public bool MapOpen = false;
	[Export]
	public bool Alive = true;
	[Export]
	public bool TileIsSlippery = false;

	private long _playerId;
	[Export]
	public long PlayerId
	{
		get => _playerId;
		set
		{
			_playerId = value;
			(InputSynchronizer ?? GetNode<MultiplayerSynchronizer>("InputSynchronizer") as InputSynchronizer).SetMultiplayerAuthority(Convert.ToInt32(_playerId));
		}
	}
	
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
	
	public bool IsMultiplayer => MultiplayerManager.Instance.MultiplayerModeEnabled;
	public bool IsOwner => Multiplayer.GetUniqueId() == PlayerId || !MultiplayerManager.Instance.MultiplayerModeEnabled;
	public bool IsHost => GetMultiplayerAuthority() == Multiplayer.GetUniqueId() || !MultiplayerManager.Instance.MultiplayerModeEnabled;
	
	// GODOT METHODS //
	public override void _Ready()
	{
		_mapScene = GD.Load<PackedScene>("res://scenes/map.tscn");
		_character = GetNode<Node2D>("Character") as Character;
		_healthBar = GetNode<TextureProgressBar>("HealthBar");
		_camera = GetNode<Camera2D>("Camera2D");
		_usernameLabel = GetNode<Label>("Username");
		_collision = GetNode<CollisionShape2D>("CollisionShape2D");
		_respawnTimer = GetNode<Timer>("RespawnTimer");
		InputSynchronizer = GetNode<MultiplayerSynchronizer>("InputSynchronizer") as InputSynchronizer;
		
		//unfortunately this does not work :c
		//var sync = GetNode<MultiplayerSynchronizer>("PlayerSynchronizer");
		//foreach(var prop in new string[] {"Direction", "position"})
		//{
			//sync.ReplicationConfig.AddProperty($".:{prop}");
			//sync.ReplicationConfig.PropertySetReplicationMode($".:{prop}", SceneReplicationConfig.ReplicationMode.Always);
		//}
		//foreach(var prop in new string[] {"Damage", "PlayerId", "Sneaking", "Attacking", "Crying", "Angried", "Shocked", "MapOpen", "Alive", "TileIsSlippery", "Username", "Health", "MaxHealth"})
		//{
			//sync.ReplicationConfig.AddProperty($".:{prop}");
			//sync.ReplicationConfig.PropertySetReplicationMode($".:{prop}", SceneReplicationConfig.ReplicationMode.OnChange);
		//}
		
		if(IsHost) {
			MaxHealth = StartMaxHealth;
			Health = StartMaxHealth;
		}
		
		if(IsOwner) {
			Node2D gameNode = GetNodeOrNull<Node2D>("/root/Game");
			if(gameNode != null) gameNode.Call("connect_player_signals", this);
			else GD.Print("Running player standalone!");
			EmitSignal(SignalName.SyncCharacter, this);
		}
		SetCamera();
	}

	public override void _PhysicsProcess(double delta)
	{
		EmitSignal(SignalName.CheckTileProperties, GlobalPosition, this);
		if(IsHost && !Alive) SetAlive();
		ApplyMovementFromInput();
		ApplyAnimations();

		if(_usernameLabel != null && GameData.Instance.PlayerName != "") Username = GameData.Instance.PlayerName;
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
	
	public void PrintNodeTree(Node root_node = null, int indent = 0)
	{
		if(root_node == null) root_node = GetTree().Root;
		
		string indent_str = "";
		for(int i = 0; i < indent; i++) indent_str += "  ";
		
		GD.Print($"{indent_str}|-- {root_node.Name}");
		
		foreach(var child in root_node.GetChildren()) PrintNodeTree(child, indent+1);
	}
	
	public void ApplyAnimations()
	{
		if(Direction.X > 0) _character.Scale = new Vector2(1, 1);
		else if(Direction.X < 0) _character.Scale = new Vector2(-1, 1);
		if(Attacking) _character.Play("Attack");
		else if(Crying) _character.Play("Cry");
		else if(Angried) _character.Play("Angry");
		else if(Shocked) _character.Play("Shock");
		else if(Sneaking) _character.Play("Sneak");
		else if(Direction != Vector2.Zero) _character.Play("Run");
		else _character.Play("Idle");
	}

	public void ApplyMovementFromInput()
	{
		Direction = IsMultiplayer ? InputSynchronizer.InputDirection : Input.GetVector("move_left", "move_right", "move_up", "move_down");
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
		
		var speed = Sneaking ? SPEED/3.0f : SPEED;
		if(Direction != Vector2.Zero)
		{
			Velocity = Direction * (float)speed;
			Crying = false; Angried = false; Shocked = false;
		}
		else if(TileIsSlippery) Velocity = new Vector2((float)Mathf.MoveToward(Velocity.X, 0, speed), (float)Mathf.MoveToward(Velocity.Y, 0, speed));
		else Velocity = Vector2.Zero;
		
		MoveAndSlide();
	}

	public void PressSneak()
	{
		Crying = false; Angried = false; Shocked = false;
		if(MapOpen) return;
		Sneaking = !Sneaking;
	}

	public void Attack()
	{
		Crying = false; Angried = false; Shocked = false;
		Attacking = !Attacking;
		Health -= 1;
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
			_map.Player = this;
			EmitSignal(SignalName.AddMap, _map);
			EmitSignal(SignalName.ShowAll, false);
		}
		else ComeBackFromMap();
	}

	public void ComeBackFromMap()
	{
		_map.QueueFree();
		_map = null;
		MapOpen = false;
		InputSynchronizer.SetMapOpenRpc(MapOpen);
		EmitSignal(SignalName.ShowAll, true);
		SetCamera();
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}

	public void MarkDead()
	{
		Alive = false;
		_collision.SetDeferred("disabled", true);
		_respawnTimer.Start();
	}

	public void Respawn()
	{
		Position = MultiplayerManager.Instance.RespawnPoint;
		_collision.SetDeferred("disabled", false);
	}

	public void SetAlive()
	{
		Alive = true;
		Engine.TimeScale = 1.0;
	}

	public void CallCharacter(Color color, string part)
	{
		_character.Call("SetHSV", part, color.H, color.S, color.V);
	}
}
