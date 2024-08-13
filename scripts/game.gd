extends Node2D

@onready var game_manager = $GameManager

@export var _attacks_spawn_node: Node2D
var attack_scene = preload("res://scenes/attack.tscn")

func _ready():
	TranslationServer.set_locale("es_ES")
	update_pickers()
	
func update_pickers():
	$"Customizer/Panel/HairPicker".color = %CustomizeCharacter.call("GetHSV","Hair")
	$"Customizer/Panel/EyesPicker".color = %CustomizeCharacter.call("GetHSV","Eyes")
	$"Customizer/Panel/SkinPicker".color = %CustomizeCharacter.call("GetHSV","Skin")
	$"Customizer/Panel/ShirtPicker".color = %CustomizeCharacter.call("GetHSV","Shirt")
	$"Customizer/Panel/HandsPicker".color = %CustomizeCharacter.call("GetHSV","Hands")
	$"Customizer/Panel/PantsPicker".color = %CustomizeCharacter.call("GetHSV","Pants")
	$"Customizer/Panel/ShoesPicker".color = %CustomizeCharacter.call("GetHSV","Shoes")

func get_character():
	return $"Customizer/Panel/CustomizeCharacter"

func _on_game_manager_update_pickers():
	update_pickers()

func connect_player_signals(player):
	print("Connecting player signals...")
	player.ShowAll.connect(_on_show_all)
	player.SyncCharacter.connect(_on_sync_character)
	player.AddMap.connect(_on_add_map)
	player.CheckTileProperties.connect(_on_check_tile_properties)
	player.DoAttack.connect(_on_attack)

func _on_show_all(show_all):
	$Players.visible = show_all
	$Map.visible = show_all

func _on_sync_character(player):
	var character_pick = get_character()
	var parts = ["Eyes", "Hair", "Skin", "Shirt", "Hands", "Pants", "Shoes"]
	for part in parts:
		player.InputSynchronizer.CallCharacterRpc(character_pick.call("GetHSV", part), part)

func _on_add_map(map):
	add_child(map, true)

func _on_check_tile_properties(global_position_player, multiplayer_controller):
	var tile_map = %TileMapMain
	var tile_coords = tile_map.local_to_map(global_position_player-tile_map.get_global_position())
	var bot_tile_data = tile_map.get_cell_tile_data(0, tile_coords)
	var top_tile_data = tile_map.get_cell_tile_data(1, tile_coords)
	var bot_tile_slippery = bool(bot_tile_data.get_custom_data("slippery")) if bot_tile_data != null else false
	var top_tile_slippery = bool(top_tile_data.get_custom_data("slippery")) if top_tile_data != null else false
	multiplayer_controller.TileIsSlippery = bot_tile_slippery || top_tile_slippery

func _on_attack(source, id: int, flip: bool, damage: int, pos: Vector2):
	print("_on_attack called")
	var to_add = attack_scene.instantiate()
	to_add.position = pos
	to_add.Damage = damage
	to_add.Flip = flip
	to_add.ReceiveSource(source)
	to_add.PlayerId = id
	to_add.name = str(id)
	_attacks_spawn_node.add_child(to_add, true)
