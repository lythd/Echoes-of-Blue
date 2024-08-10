using Godot;
using System;

public interface IDamageableEntity
{
	public int Health { get; set; }
	public int MaxHealth { get; set; }
	public int Damage { get; set; }
}
