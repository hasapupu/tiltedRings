using Godot;
using System;
using System.Collections;

public partial class ring : Sprite2D
{
	Sprite2D parent;
	
	public override void _Ready()
	{
		parent = GetParent() as Sprite2D;
	}
	
	public override void _Process(double delta)
	{
		Rotation = -(parent.Rotation);
	}
}
