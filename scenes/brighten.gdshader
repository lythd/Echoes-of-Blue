shader_type canvas_item;

varying flat vec4 modulate;

// i love you @rylydou ur my hero (the regular *= one wouldn't let me update it after for some reason so i needed this one)
// https://github.com/godotengine/godot/issues/49781#issuecomment-1543250039

void vertex() {
	modulate = COLOR;
}

void fragment() {
	vec4 color = texture(TEXTURE, UV) * modulate;
	COLOR = vec4(color.r*2.,color.g*2., color.b*2., color.a);
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
