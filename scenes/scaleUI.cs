using Godot;
using System;

public partial class scaleUI : Node2D
{
	public override void _Process(double delta)
	{
		Vector2 windowSize = DisplayServer.WindowGetSize();
		GlobalPosition = new Vector2((windowSize.X / 20) * 2,(float)((windowSize.Y / 10) * 7.5));
	}
}
