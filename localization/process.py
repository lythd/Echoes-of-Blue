def replace(filename, desc, name):
	print(f"Working on {filename} ...")
	with open(filename, "r") as file:
		lines = file.readlines()

	with open(filename, "w") as file:
		for i, line in enumerate(lines):
			if i % 2 == 0 and i != 0:
				file.write(line.split(",", 1)[0] + desc + "," + line.split(",", 1)[1])
			elif i % 2 == 1:
				file.write(line.split(",", 1)[0] + name + "," + line.split(",", 1)[1])
			else:
				file.write(line)

replace("tiles_localization.csv", "_TILE_DESC", "_TILE_NAME")
replace("items_localization.csv", "_ITEM_DESC", "_ITEM_NAME")
replace("effects_localization.csv", "_EFFECT_DESC", "_EFFECT_NAME")
replace("mobs_localization.csv", "_MOB_DESC", "_MOB_NAME")
replace("locations_localization.csv", "_LOCATION_DESC", "_LOCATION_NAME")
replace("machines_localization.csv", "_MACHINE_DESC", "_MACHINE_NAME")
replace("countries_localization.csv", "_COUNTRY_DESC", "_COUNTRY_NAME")
replace("categories_localization.csv", "_CATEGORY_DESC", "_CATEGORY_NAME")
