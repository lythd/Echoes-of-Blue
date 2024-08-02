extends Node

signal update_pickers

# All the steam networking stuff came from this tutorial since I wasn't able to get it myself:
# https://www.youtube.com/watch?v=xugYYCz0VHU
# This was in turn based on Brackeys first game, although the only things that would have been taken from it are the free to use sounds and the font
# Though obviously I had to do a lot of changes to adapt it to my game

var multiplayer_scene = preload("res://scenes/multiplayer_player.tscn")

func _ready():
	if OS.has_feature("dedicated_server"):
		print("Starting dedicated server...")
		%NetworkManager.become_host(true)
	use_steam()
	%SteamHUD.get_node("Panel/VersionInfo").clear()
	%SteamHUD.get_node("Panel/VersionInfo").append_text("[center]" + MultiplayerManager.GAME_VERSION + "#" + MultiplayerManager.GAME_HASH.substr(0,8) + "[/center]")

func start_solo():
	MultiplayerManager.multiplayer_mode_enabled = false
	print("Start single player pressed")
	%SteamHUD.hide()
	%Customizer.hide()
	var id = -1
	print("Player %s joined the game!" % id)
	var player_to_add = multiplayer_scene.instantiate()
	player_to_add.player_id = id
	player_to_add.name = str(id)
	%NetworkManager._players_spawn_node.add_child(player_to_add, true)
	

func become_host():
	MultiplayerManager.multiplayer_mode_enabled = true
	print("Become host pressed")
	%SteamHUD.hide()
	%Customizer.hide()
	%NetworkManager.become_host()
	
func join_as_client():
	MultiplayerManager.multiplayer_mode_enabled = true
	print("Join as another player")
	join_lobby()

func use_steam():
	print("Using Steam!")
	%SteamHUD.show()
	%Customizer.show()
	SteamManager.initialize_steam()
	Steam.lobby_match_list.connect(_on_lobby_match_list)
	%NetworkManager.active_network_type = %NetworkManager.MULTIPLAYER_NETWORK_TYPE.STEAM

func list_steam_lobbies():
	print("List Steam lobbies")
	%NetworkManager.list_lobbies()

func join_lobby(lobby_id = 0):
	if %NetworkManager.join_as_client(lobby_id):
		print("Joining lobby %s" % lobby_id)
		%SteamHUD.hide()
		%Customizer.hide()
	else:
		print("This lobby's version is incompatible with your own.")

func _on_lobby_match_list(lobbies: Array):
	print("On lobby match list")
	
	for lobby_child in $"../SteamHUD/Panel/Lobbies/VBoxContainer".get_children():
		lobby_child.queue_free()
		
	for lobby in lobbies:
		var lobby_name: String = Steam.getLobbyData(lobby, "name")
		
		if lobby_name != "":
			var lobby_mode: String = Steam.getLobbyData(lobby, "mode")
			var lobby_ver: String = Steam.getLobbyData(lobby, "version")
			var lobby_hash: String = Steam.getLobbyData(lobby, "hash")
			
			var lobby_button: Button = Button.new()
			lobby_button.set_text(lobby_name + " | " + lobby_mode + " | " + lobby_ver + "#" + lobby_hash.substr(0,8))
			lobby_button.set_size(Vector2(100, 30))
			lobby_button.add_theme_font_size_override("font_size", 8)
			
			var fv = FontVariation.new()
			fv.set_base_font(load("res://assets/fonts/PixelOperator8.ttf"))
			lobby_button.add_theme_font_override("font", fv)
			lobby_button.set_name("lobby_%s" % lobby)
			lobby_button.alignment = HORIZONTAL_ALIGNMENT_LEFT
			lobby_button.connect("pressed", Callable(self, "join_lobby").bind(lobby))
			
			$"../SteamHUD/Panel/Lobbies/VBoxContainer".add_child(lobby_button)



func _on_random_pressed():
	%CustomizeCharacter.call("Randomize")
	emit_signal("update_pickers")
func _on_special_left_pressed():
	%CustomizeCharacter.call("AdjInd","Special",-1);
func _on_special_right_pressed():
	%CustomizeCharacter.call("AdjInd","Special",1);
func _on_hair_left_pressed():
	%CustomizeCharacter.call("AdjInd","Hair",-1);
func _on_hair_right_pressed():
	%CustomizeCharacter.call("AdjInd","Hair",1);
func _on_eyes_left_pressed():
	%CustomizeCharacter.call("AdjInd","Eyes",-1);
func _on_eyes_right_pressed():
	%CustomizeCharacter.call("AdjInd","Eyes",1);
func _on_skin_left_pressed():
	%CustomizeCharacter.call("AdjInd","Skin",-1);
func _on_skin_right_pressed():
	%CustomizeCharacter.call("AdjInd","Skin",1);
func _on_shirt_left_pressed():
	%CustomizeCharacter.call("AdjInd","Shirt",-1);
func _on_shirt_right_pressed():
	%CustomizeCharacter.call("AdjInd","Shirt",1);
func _on_hands_left_pressed():
	%CustomizeCharacter.call("AdjInd","Hands",-1);
func _on_hands_right_pressed():
	%CustomizeCharacter.call("AdjInd","Hands",1);
func _on_pants_left_pressed():
	%CustomizeCharacter.call("AdjInd","Pants",-1);
func _on_pants_right_pressed():
	%CustomizeCharacter.call("AdjInd","Pants",1);
func _on_shoes_left_pressed():
	%CustomizeCharacter.call("AdjInd","Shoes",-1);
func _on_shoes_right_pressed():
	%CustomizeCharacter.call("AdjInd","Shoes",1);
func _on_hair_picker_color_changed(color):
	%CustomizeCharacter.call("SetHSV","Hair",color.h,color.s,color.v);
func _on_eyes_picker_color_changed(color):
	%CustomizeCharacter.call("SetHSV","Eyes",color.h,color.s,color.v);
func _on_skin_picker_color_changed(color):
	%CustomizeCharacter.call("SetHSV","Skin",color.h,color.s,color.v);
func _on_shirt_picker_color_changed(color):
	%CustomizeCharacter.call("SetHSV","Shirt",color.h,color.s,color.v);
func _on_hands_picker_color_changed(color):
	%CustomizeCharacter.call("SetHSV","Hands",color.h,color.s,color.v);
func _on_pants_picker_color_changed(color):
	%CustomizeCharacter.call("SetHSV","Pants",color.h,color.s,color.v);
func _on_shoes_picker_color_changed(color):
	%CustomizeCharacter.call("SetHSV","Shoes",color.h,color.s,color.v);
