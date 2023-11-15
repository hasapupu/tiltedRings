using Godot;
using System;

public partial class LS_hitbox : Area3D
{
	private void _on_area_entered(Area3D area)
	{
		GD.Print("nahhhhhhh");
		if(area.IsInGroup("enemy"))
		{
			GD.Print("Hit registered");
		}
	}
	
	public override void _Process(double delta)
	{
		//GD.Print("Script active");
	}
}


