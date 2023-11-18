using Godot;
using System;

public partial class BW_pickUp : demoPickup
{
	public override void _Ready()
	{
		newState = weaponState.BOW;
	}
}
