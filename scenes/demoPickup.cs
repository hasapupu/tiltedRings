using Godot;
using System;

public partial class demoPickup : Area3D
{
	private void _OnBodyEntered(Node3D body)
	{
		if(body is playerScript)
		{
			GD.Print("Gone");
			GetParent().QueueFree();
		}
	}
}



