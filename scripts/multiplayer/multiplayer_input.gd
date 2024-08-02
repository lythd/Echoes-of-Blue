extends MultiplayerSynchronizer

@onready var player = $".."

var input_direction
var username = ""

func _ready():
	if get_multiplayer_authority() != multiplayer.get_unique_id():
		set_process(false)
		set_physics_process(false)

	input_direction = Input.get_vector("move_left", "move_right", "move_up", "move_down")
	username = SteamManager.steam_username

func _physics_process(_delta):
	input_direction = Input.get_vector("move_left", "move_right", "move_up", "move_down")

func _process(_delta):
	if Input.is_action_just_pressed("sneak"):
		sneak.rpc()
	if Input.is_action_just_pressed("attack"):
		attack.rpc()
	if Input.is_action_just_pressed("cry"):
		cry.rpc()
	if Input.is_action_just_pressed("angry"):
		angry.rpc()
	if Input.is_action_just_pressed("shock"):
		shock.rpc()

@rpc("call_local")
func sneak():
	if multiplayer.is_server():
		player.press_sneak()

@rpc("call_local")
func attack():
	if multiplayer.is_server():
		player.attack()

@rpc("call_local")
func cry():
	if multiplayer.is_server():
		player.cry()

@rpc("call_local")
func angry():
	if multiplayer.is_server():
		player.angry()

@rpc("call_local")
func shock():
	if multiplayer.is_server():
		player.shock()

@rpc("call_local")
func set_map_open(val: bool):
	if multiplayer.is_server():
		player._set_map_open(val)
