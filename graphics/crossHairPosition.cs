using Godot;
using System;

public partial class crossHairPosition : MeshInstance2D
{
	public override void _Process(double delta)
	{
		Vector2 windowSize = DisplayServer.WindowGetSize();
		GlobalPosition = new Vector2(windowSize.X / 2, windowSize.Y / 2);
	}
}
