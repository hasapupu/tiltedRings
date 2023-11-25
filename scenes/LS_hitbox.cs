using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class LS_hitbox : Area3D
{
	public float damage = 20f;
	enemyMovementBaseClas enemyBody;
	private void _on_area_entered(Area3D area)
	{
		GD.Print("nahhhhhhh");
		if(area.IsInGroup("enemy"))
		{
			GD.Print("Hit registered");
			var enemy = area as enemyHitboxBaseClass;
			enemyBody = area.GetParent() as enemyMovementBaseClas;
			enemyBody.isHit = true;
			enemyBody.Velocity = Vector3.Zero;
			enemyBody.Velocity -= 20*(GetParent().GetParent().GetParent().GetParent().GetParent().GetParent().GetParent<Node3D>().GlobalTransform.Origin - enemyBody.GlobalTransform.Origin);
			enemy.hp -= damage;
			DelayMethod(0.08f);
			
			GD.Print("AAAAAAAAAAAAAAAAAAAA");

		}
	}
	
	private async void DelayMethod(float amount)
	{
		await ToSignal(GetTree().CreateTimer(amount), "timeout");
		enemyBody.isHit = false;
	}

}
