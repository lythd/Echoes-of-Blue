shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec4 col = texture(TEXTURE, UV);
	col.a = 1. - step(1,col.a);
	COLOR = col;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
