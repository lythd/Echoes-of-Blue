using System;
using System.Collections.Generic;
using EchoesofBlue.scripts.stuff;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

[JsonConverter(typeof(CoordinateIConverter))]
public class CoordinateI
{
	public int X { get; set; }
	public int Y { get; set; }

	public Vector2I V
	{
		get => new Vector2I(X, Y);
		set
		{
			X = value.X;
			Y = value.Y;
		}
	}
}