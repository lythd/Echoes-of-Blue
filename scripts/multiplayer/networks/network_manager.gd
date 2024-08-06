extends Node

signal add_player(player)

enum MULTIPLAYER_NETWORK_TYPE { ENET, STEAM }

@export var _players_spawn_node: Node2D

var active_network_type: MULTIPLAYER_NETWORK_TYPE = MULTIPLAYER_NETWORK_TYPE.ENET
var steam_network_scene := preload("res://scenes/multiplayer/networks/steam_network.tscn")
var active_network

func _build_multiplayer_network():
	if not active_network:
		print("Setting active_network")
		
		MultiplayerManager.multiplayer_mode_enabled = true
		
		match active_network_type:
			MULTIPLAYER_NETWORK_TYPE.STEAM:
				print("Setting network type to Steam")
				_set_active_network(steam_network_scene)
			_:
				print("No match for network type!")

func _set_active_network(active_network_scene):
	var network_scene_initialized = active_network_scene.instantiate()
	active_network = network_scene_initialized
	active_network._players_spawn_node = _players_spawn_node
	active_network.add_player.connect(_on_add_player)
	add_child(active_network)

func _on_add_player(player):
	add_player.emit(player)

func become_host(is_dedicated_server = false):
	_build_multiplayer_network()
	MultiplayerManager.host_mode_enabled = true if is_dedicated_server == false else false
	active_network.become_host()
	
func join_as_client(lobby_id = 0) -> bool:
	_build_multiplayer_network()
	return active_network.join_as_client(lobby_id)
	
func list_lobbies():
	_build_multiplayer_network()
	active_network.list_lobbies()
