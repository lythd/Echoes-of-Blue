using Godot;
using System;

public class GameEntity
{
	public string Id { get; protected set; }
	
	public virtual string Name { get; protected set; }
	
	public virtual string Desc { get; protected set; }
}
