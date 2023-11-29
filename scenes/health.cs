using Godot;
using System;

public partial class health : Control
{
	public override void _Process(double delta)
	{
		Vector2 windowSize = DisplayServer.WindowGetSize();
		GlobalPosition = new Vector2((windowSize.X / 20),(float)((windowSize.Y / 10) * 3.8));
	}
}
