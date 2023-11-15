using Godot;
using System;

public partial class demoPickup : Area3D
{
	private void _OnBodyEntered(Node3D body)
	{
		if(body is playerScript)
		{
			var player = body as playerScript;
			player.setWeaponState(weaponState.LONGSWORD);
			GD.Print("Gone");
			GetParent().QueueFree();
		}
	}
}



