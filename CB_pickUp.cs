using Godot;
using System;

public partial class CB_pickUp : demoPickup
{
	public override void _Ready()
	{
		newState = weaponState.CROSSBOW;
	}
}
