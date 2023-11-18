using Godot;
using System;

public partial class SC_pickUp : demoPickup
{
	public override void _Ready()
	{
		newState = weaponState.SCEMITAR;
	}
}
