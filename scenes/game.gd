extends Node2D

@onready var game_manager = $GameManager

func _ready():
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
