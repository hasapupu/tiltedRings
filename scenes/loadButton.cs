using Godot;
using System;

public partial class loadButton : Button
{
	private void _on_pressed()
	{
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://scenes/world.tscn").Instantiate());
		GetParent().QueueFree();
	}
	
	public override void _Ready()
	{
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
	}
}



