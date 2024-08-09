extends CharacterBody2D

signal show_all(show_all)
signal sync_character(player)
signal add_map(map)
signal check_tile_properties(global_position_player, multiplayer_controller)

const SPEED = 75.0

var map_scene = preload("res://scenes/map.tscn")
var map = null

@onready var character = $Character

var direction
@export var sneaking = false
@export var attacking = false
@export var crying = false
@export var angried = false
@export var shocked = false
@export var map_open = false
var alive = true

@onready var username_label = $Username

var tile_is_slippery = false

@export var player_id := 1:
	set(id):
		player_id = id
		$InputSynchronizer.set_multiplayer_authority(id)

@export var start_max_health : int = 20

@export var health: int:
	get:
		return health
	set(value):
		health = value
		%HealthBar.value = health
		
@export var max_health: int:
	get:
		return max_health
	set(value):
		max_health = value
		%HealthBar.min_value = 3. * max_health/(6. - %HealthBar.size.x)
		%HealthBar.max_value = max_health - %HealthBar.min_value
		if health > max_health:
			health = max_health


func _ready():
	if get_multiplayer_authority() == multiplayer.get_unique_id() || !MultiplayerManager.multiplayer_mode_enabled:
		max_health = start_max_health
		health = start_max_health
	if in_control():
		var game_node = get_node_or_null("/root/Game")
		if game_node != null:
			game_node.call("connect_player_signals", self)
		else:
			print("Running player standalone!")
		sync_character.emit(self)
	set_camera()


func set_camera():
	if in_control():
		$Camera2D.make_current()
		$Camera2D.enabled = true
	else:
		$Camera2D.enabled = false

func print_node_tree(root_node=null, indent = 0):
	if root_node == null:
		root_node = get_tree().get_root()

	var indent_str = ""
	for i in range(indent):
		indent_str += "  "

	print(indent_str + "|-- " + root_node.name)

	for child in root_node.get_children():
		print_node_tree(child, indent + 1)

func in_control() -> bool:
	return multiplayer.get_unique_id() == player_id || !MultiplayerManager.multiplayer_mode_enabled

func _apply_animations(_delta):
	if direction.x > 0:
		character.scale.x = 1
	elif direction.x < 0:
		character.scale.x = -1
	if attacking:
		character.Play("Attack")
	elif crying:
		character.Play("Cry")
	elif angried:
		character.Play("Angry")
	elif shocked:
		character.Play("Shock")
	elif sneaking:
		character.Play("Sneak")
	elif direction:
		character.Play("Run")
	else:
		character.Play("Idle")

func _apply_movement_from_input(delta):
	var is_server = MultiplayerManager.multiplayer_mode_enabled
	direction = $InputSynchronizer.input_direction if is_server else Input.get_vector("move_left", "move_right", "move_up", "move_down")
	if is_server:
		GameData.PlayerName = $InputSynchronizer.username
	if map_open:
		direction = Vector2.ZERO
	
	# client side controls
	if in_control() && Input.is_action_just_pressed("map"):
		press_map()
	if in_control() && Input.is_action_just_pressed("debug_print_scene_tree"):
		print_node_tree()
	
	# singleplayer versions of host side controls
	if !is_server && Input.is_action_just_pressed("sneak"): 
		press_sneak()
	if !is_server && Input.is_action_just_pressed("attack"):
		attack()
	if !is_server && Input.is_action_just_pressed("cry"):
		cry()
	if !is_server && Input.is_action_just_pressed("angry"):
		angry()
	if !is_server && Input.is_action_just_pressed("shock"):
		shock()
	
	var speed = SPEED/3 if sneaking else SPEED
	if direction:
		velocity = direction * speed
		crying = false;angried = false;shocked = false
	elif tile_is_slippery:
		velocity.x = move_toward(velocity.x, 0, speed * delta)
		velocity.y = move_toward(velocity.y, 0, speed * delta)
	else:
		velocity.x = 0
		velocity.y = 0
	
	move_and_slide()

func press_sneak():
	crying = false;angried = false;shocked = false
	if map_open:
		return
	sneaking = !sneaking

func attack():
	crying = false;angried = false;shocked = false
	attacking = !attacking
	health -= 1;

func cry():
	angried = false;shocked = false
	crying = !crying

func angry():
	crying = false;shocked = false
	angried = !angried

func shock():
	crying = false;angried = false
	shocked = !shocked

func press_map():
	crying = false;angried = false;shocked = false
	map_open = map == null
	$InputSynchronizer.set_map_open.rpc(map_open)
	if map_open:
		map = map_scene.instantiate()
		map.Controller = self
		add_map.emit(map)
		show_all.emit(false)
	else:
		come_back_from_map()

func _set_map_open(val: bool):
	map_open = val

func come_back_from_map():
	map.queue_free()
	map = null
	map_open = false
	$InputSynchronizer.set_map_open.rpc(map_open)
	show_all.emit(true)
	set_camera()
	Input.mouse_mode = Input.MOUSE_MODE_VISIBLE;

func _check_tile_properties():
	check_tile_properties.emit(get_global_position(), self)

func _physics_process(delta):
	_check_tile_properties()
	if multiplayer.is_server():
		if not alive:
			_set_alive()
	_apply_movement_from_input(delta)
	_apply_animations(delta)

	if username_label && GameData.PlayerName != "":
		username_label.set_text(GameData.PlayerName)

func mark_dead():
	alive = false
	$CollisionShape2D.set_deferred("disabled", true)
	$RespawnTimer.start()

func _respawn():
	position = MultiplayerManager.respawn_point
	$CollisionShape2D.set_deferred("disabled", false)

func _set_alive():
	alive = true
	Engine.time_scale = 1.0

func call_character(color, part):
	character.call("SetHSV", part, color.h, color.s, color.v)

func get_input_synchronizer():
	return $InputSynchronizer
