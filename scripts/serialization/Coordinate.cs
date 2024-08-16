using System;
using System.Collections.Generic;
using EchoesofBlue.scripts.stuff;
using Godot;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

[JsonConverter(typeof(CoordinateConverter))]
public class Coordinate
{
	public float X { get; set; }
	public float Y { get; set; }

	public Vector2 V
	{
		get => new Vector2(X, Y);
		set
		{
			X = value.X;
			Y = value.Y;
		}
	}
}