using Godot;
using System;

public partial class demoPickup : Area3D
{
	protected weaponState newState = weaponState.LONGSWORD;
	private void _OnBodyEntered(Node3D body)
	{
		
		if(body is playerScript)
		{
			var player = body as playerScript;
			player.setWeaponState(newState);
			GD.Print("Gone");
			GetParent().QueueFree();
		}
	}
}
