extends CharacterBody2D

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
var username = ""

var tile_map: TileMap = null
var tile_is_slippery = false

@export var player_id := 1:
	set(id):
		player_id = id
		$InputSynchronizer.set_multiplayer_authority(id)

func _ready():
	if in_control():
		$Camera2D.make_current()
		$Camera2D.enabled = true
		var game = $"../.."
		if game != null:
			var character_pick = game.call("get_character")
			var color = character_pick.call("GetHSV", "Hair")
			character.call("SetHSV", "Hair", color.h, color.s, color.v)
			color = character_pick.call("GetHSV", "Eyes")
			character.call("SetHSV", "Eyes", color.h, color.s, color.v)
			color = character_pick.call("GetHSV", "Skin")
			character.call("SetHSV", "Skin", color.h, color.s, color.v)
			color = character_pick.call("GetHSV", "Shirt")
			character.call("SetHSV", "Shirt", color.h, color.s, color.v)
			color = character_pick.call("GetHSV", "Hands")
			character.call("SetHSV", "Hands", color.h, color.s, color.v)
			color = character_pick.call("GetHSV", "Pants")
			character.call("SetHSV", "Pants", color.h, color.s, color.v)
			color = character_pick.call("GetHSV", "Shoes")
			character.call("SetHSV", "Shoes", color.h, color.s, color.v)
	else:
		$Camera2D.enabled = false

func in_control() -> bool:
	return multiplayer.get_unique_id() == player_id || !MultiplayerManager.multiplayer_mode_enabled

func print_node_tree(root_node=null, indent = 0):
	if root_node == null:
		root_node = get_tree().get_root()

	var indent_str = ""
	for i in range(indent):
		indent_str += "  "

	print(indent_str + "|-- " + root_node.name)

	for child in root_node.get_children():
		print_node_tree(child, indent + 1)

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
	username = $InputSynchronizer.username if is_server else GameData.PlayerName
	if map_open:
		direction = Vector2.ZERO
	
	# the map is always handled client side so instead of checking for singleplayer we just check if this is the client
	if in_control() && Input.is_action_just_pressed("map"):
		press_map()
	
	if !is_server && Input.is_action_just_pressed("sneak"): # for singleplayer
		press_sneak()
	if !is_server && Input.is_action_just_pressed("attack"): # for singleplayer
		attack()
	if !is_server && Input.is_action_just_pressed("cry"): # for singleplayer
		cry()
	if !is_server && Input.is_action_just_pressed("angry"): # for singleplayer
		angry()
	if !is_server && Input.is_action_just_pressed("shock"): # for singleplayer
		shock()
	
	var speed = SPEED/3 if sneaking else SPEED
	if direction:
		velocity = direction * speed
		crying = false
		angried = false
		shocked = false
	elif tile_is_slippery:
		velocity.x = move_toward(velocity.x, 0, speed * delta)
		velocity.y = move_toward(velocity.y, 0, speed * delta)
	else:
		velocity.x = 0
		velocity.y = 0
	
	move_and_slide()

func press_sneak():
	crying = false
	angried = false
	shocked = false
	if map_open:
		return
	sneaking = !sneaking

func attack():
	crying = false
	angried = false
	shocked = false
	attacking = !attacking

func cry():
	angried = false
	shocked = false
	crying = !crying

func angry():
	crying = false
	shocked = false
	angried = !angried

func shock():
	crying = false
	angried = false
	shocked = !shocked

func press_map():
	crying = false
	angried = false
	shocked = false
	map_open = map == null
	$InputSynchronizer.set_map_open.rpc(map_open)
	if map_open:
		map = map_scene.instantiate()
		map.Controller = self
		$"../..".add_child(map, true)
		hide()
	else:
		come_back_from_map()

func _set_map_open(val: bool):
	map_open = val

func come_back_from_map():
	map.queue_free()
	map = null
	map_open = false
	$InputSynchronizer.set_map_open.rpc(map_open)
	show()
	_ready()
	Input.mouse_mode = Input.MOUSE_MODE_VISIBLE;

func _check_tile_properties():
	if tile_map == null:
		tile_map = $"../../TextureRectMain/TileMapMain"
	if tile_map == null:
		return
	var tile_coords = tile_map.local_to_map(get_global_position()-tile_map.get_global_position())
	var bot_tile_data = tile_map.get_cell_tile_data(0, tile_coords)
	var top_tile_data = tile_map.get_cell_tile_data(1, tile_coords)
	var bot_tile_slippery = bool(bot_tile_data.get_custom_data("slippery")) if bot_tile_data != null else false
	var top_tile_slippery = bool(top_tile_data.get_custom_data("slippery")) if top_tile_data != null else false
	tile_is_slippery = bot_tile_slippery || top_tile_slippery

func _physics_process(delta):
	_check_tile_properties()
	if multiplayer.is_server():
		if not alive:
			_set_alive()
	_apply_movement_from_input(delta)
	_apply_animations(delta)

	if username_label && username != "":
		username_label.set_text(username)

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
