using Godot;
using System;

public partial class LS_hitbox : Area3D
{
	public float damage = 20f;
	private void _on_area_entered(Area3D area)
	{
		GD.Print("nahhhhhhh");
		if(area.IsInGroup("enemy"))
		{
			GD.Print("Hit registered");
			var enemy = area as enemyHitboxBaseClass;
			enemy.hp -= damage;
		}
	}
}
