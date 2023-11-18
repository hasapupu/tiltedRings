using Godot;
using System;

public partial class SS_pickUp : demoPickup
{
	public override void _Ready()
	{
		newState = weaponState.SHORTSWORD;
	}
}
