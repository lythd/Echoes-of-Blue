extends Node

var host_mode_enabled = false
var multiplayer_mode_enabled = false
var respawn_point = Vector2(30, 20)

const GAME_VERSION = "v0.0.1"
@onready var GAME_HASH = calculate_game_hash()

func calculate_game_hash() -> String:
	return "AAAAAAAA".sha256_text()

func compare_versions(game_version: String, game_hash: String) -> bool:
	var their_version = parse_version(game_version)
	var our_version = parse_version(GAME_VERSION)
	var alike = their_version["phase"] == our_version["phase"]
	alike = alike and their_version["major"] == our_version["major"]
	alike = alike and game_hash == GAME_HASH
	return alike

func parse_version(version: String) -> Dictionary:
	var version_strip = version.strip_edges().trim_prefix("v").trim_suffix("d")
	var parts = version_strip.split('.', false)
	var phase = ""
	var major = 0
	var minor = 0
	var dev = version.contains('d')
	if version_strip.begins_with("0.0"):
		phase = "alpha"
		major = int(parts[2])
		if len(parts) > 3:
			minor = int(parts[3])
	elif version_strip.begins_with("0"):
		phase = "beta"
		major = int(parts[1])
		if len(parts) > 2:
			minor = int(parts[2])
	else:
		phase = "release"
		major = int(parts[1])
		if len(parts) > 2:
			minor = int(parts[2])
	return {"phase":phase,
			"major":major,
			"minor":minor,
			"development":dev}
