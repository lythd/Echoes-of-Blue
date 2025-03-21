extends Node

var multiplayer_peer: SteamMultiplayerPeer = SteamMultiplayerPeer.new()
var _players_spawn_node
var _hosted_lobby_id = 0
var network_manager

const LOBBY_NAME = "SDOFUNzDOU3"
const LOBBY_MODE = "CO_OP"

func  _ready():
	Steam.lobby_created.connect(_on_lobby_created.bind())

func become_host():
	print("Starting host!")
	
	multiplayer.peer_connected.connect(_add_player_to_game)
	multiplayer.peer_disconnected.connect(_del_player)
	
	Steam.lobby_joined.connect(_on_lobby_joined.bind())
	Steam.createLobby(Steam.LOBBY_TYPE_PUBLIC, SteamManager.LobbyMaxMembers)
	
func join_as_client(lobby_id) -> bool:
	print("Joining lobby %s" % lobby_id)
	var same_ver = MultiplayerManager.CompareVersions(Steam.getLobbyData(lobby_id, "version"), Steam.getLobbyData(lobby_id, "hash"))
	if same_ver:
		Steam.lobby_joined.connect(_on_lobby_joined.bind())
		Steam.joinLobby(int(lobby_id))
	return same_ver

func _on_lobby_created(connection: int, lobby_id):
	print("On lobby created")
	if connection == 1:
		_hosted_lobby_id = lobby_id
		print("Created lobby: %s" % _hosted_lobby_id)
		
		Steam.setLobbyJoinable(_hosted_lobby_id, true)
		
		Steam.setLobbyData(_hosted_lobby_id, "name", LOBBY_NAME)
		Steam.setLobbyData(_hosted_lobby_id, "mode", LOBBY_MODE)
		Steam.setLobbyData(_hosted_lobby_id, "version", MultiplayerManager.GAME_VERSION)
		Steam.setLobbyData(_hosted_lobby_id, "hash", MultiplayerManager.GAME_HASH)
		
		_create_host()
	else:
		print("Error %s with lobby %s." % [connection, lobby_id])
		if connection == 3:
			print("This error in particular might mean you have to restart steam and/or your computer.")
		else:
			print("A good bet is always to restart steam and/or your computer.")
		var FAIL_REASON: String
		match connection:
			2:  FAIL_REASON = "This lobby no longer exists."
			3:  FAIL_REASON = "You don't have permission to join this lobby."
			4:  FAIL_REASON = "The lobby is now full."
			5:  FAIL_REASON = "Uh... something unexpected happened!"
			6:  FAIL_REASON = "You are banned from this lobby."
			7:  FAIL_REASON = "You cannot join due to having a limited account."
			8:  FAIL_REASON = "This lobby is locked or disabled."
			9:  FAIL_REASON = "This lobby is community locked."
			10: FAIL_REASON = "A user in the lobby has blocked you from joining."
			11: FAIL_REASON = "A user you have blocked is in the lobby."
		print("That error could potentially be: % Though really I have no idea I can't find these error codes anywhere idk if its the same for lobby create and lobby join. If its not the listed reason my best guess is another lobby exists with the same lobby name." % FAIL_REASON)

func _create_host():
	print("Create Host")
	
	var error = multiplayer_peer.create_host(0, [])
	
	if error == OK:
		multiplayer.set_multiplayer_peer(multiplayer_peer)
		
		if not OS.has_feature("dedicated_server"):
			_add_player_to_game(1) # THIS NEEDS TO BE 1, PEER MULTIPLAYER AUTHORITY NEEDS TO BE 1 FOR THE HOST
	else:
		print("error creating host: %s" % str(error))

func _on_lobby_joined(lobby: int, _permissions: int, _locked: bool, response: int):
	print("On lobby joined")
	
	if response == 1:
		var id = Steam.getLobbyOwner(lobby)
		if id != Steam.getSteamID():
			print("Connecting client to socket...")
			connect_socket(id)
	else:
		# Get the failure reason
		var FAIL_REASON: String
		match response:
			2:  FAIL_REASON = "This lobby no longer exists."
			3:  FAIL_REASON = "You don't have permission to join this lobby."
			4:  FAIL_REASON = "The lobby is now full."
			5:  FAIL_REASON = "Uh... something unexpected happened!"
			6:  FAIL_REASON = "You are banned from this lobby."
			7:  FAIL_REASON = "You cannot join due to having a limited account."
			8:  FAIL_REASON = "This lobby is locked or disabled."
			9:  FAIL_REASON = "This lobby is community locked."
			10: FAIL_REASON = "A user in the lobby has blocked you from joining."
			11: FAIL_REASON = "A user you have blocked is in the lobby."
		print(FAIL_REASON)
	
func connect_socket(steam_id: int):
	var error = multiplayer_peer.create_client(steam_id, 0, [])
	if error == OK:
		print("Connecting peer to host...")
		multiplayer.set_multiplayer_peer(multiplayer_peer)
	else:
		print("Error creating client: %s" % str(error))

func list_lobbies():
	Steam.addRequestLobbyListDistanceFilter(Steam.LOBBY_DISTANCE_FILTER_WORLDWIDE)
	# NOTE: If you are using the test app id, you will need to apply a filter on your game name
	# Otherwise, it may not show up in the lobby list of your clients
	Steam.addRequestLobbyListStringFilter("name", LOBBY_NAME, Steam.LOBBY_COMPARISON_EQUAL)
	Steam.requestLobbyList()

func _add_player_to_game(id: int):
	network_manager.add_player(id)
	
func _del_player(id: int):
	print("Player %s left the game!" % id)
	if not _players_spawn_node.has_node(str(id)):
		return
	_players_spawn_node.get_node(str(id)).queue_free()
