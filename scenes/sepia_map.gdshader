shader_type canvas_item;

void fragment() {
    vec4 col = texture(TEXTURE, UV);
    
	// reduce contrast (didn't end up liking this)
	//vec3 avgCol = (texture(TEXTURE, UV + 1.0 / float(textureSize(TEXTURE, 0).x) * vec2(-1, 0)).rgb +
    //           texture(TEXTURE, UV + 1.0 / float(textureSize(TEXTURE, 0).x) * vec2(1, 0)).rgb +
    //           texture(TEXTURE, UV + 1.0 / float(textureSize(TEXTURE, 0).y) * vec2(0, -1)).rgb +
    //           texture(TEXTURE, UV + 1.0 / float(textureSize(TEXTURE, 0).y) * vec2(0, 1)).rgb + col.rgb) / 5.0;

    //float diff = length(col.rgb - avgCol);
    //col.rgb = mix(avgCol, col.rgb, 1.-pow(1.-diff, 2.));
    
    // apply sepia tone
    col.rgb = vec3(
        col.r * 0.393 + col.g * 0.769 + col.b * 0.189,
        col.r * 0.349 + col.g * 0.686 + col.b * 0.168,
        col.r * 0.272 + col.g * 0.534 + col.b * 0.131
    );
    
    // darken a lil
    col.rgb = col.rgb * 0.7;// * (1.0-diff);
	//col.r = diff; // just visualization stuff
	
    COLOR = col;
}