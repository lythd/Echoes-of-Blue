shader_type canvas_item;

//uniform sampler2D tex : source_color;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
    vec4 col = texture(TEXTURE, UV);
    float sum = col.r + col.g + col.b + 1. - col.a; // sum == 0 -> black so take the screenuv texture
	//col.r = step(0.,-sum)*texture(tex, SCREEN_UV).r + col.r*(1.-step(0.,-sum));
	//col.g = step(0.,-sum)*texture(tex, SCREEN_UV).g + col.g*(1.-step(0.,-sum));
	//col.b = step(0.,-sum)*texture(tex, SCREEN_UV).b + col.b*(1.-step(0.,-sum));
	//col = texture(tex, SCREEN_UV);
	col.a = (1.-step(0.,-sum))*col.a;
	COLOR = col;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
