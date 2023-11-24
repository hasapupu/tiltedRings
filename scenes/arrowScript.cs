using Godot;
using System;

public partial class arrowScript : Node3D
{
	Node3D arrow;
	RayCast3D ray;
	public float damage = 0f;
	
	public override void _Ready()
	{
		ray = GetNode("RayCast3D") as RayCast3D;
		arrow = GetNode("arrow") as Node3D;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Position += Transform.Basis * new Vector3(0,0,40f) * (float)delta;
	}
	
	public override void _Process(double delta)
	{
		if (ray.IsColliding())
		{
			var temp = ray.GetCollider() as Node3D;
			if(temp.IsInGroup("enemy"))
			{
				var enemy = temp as enemyHitboxBaseClass;
				enemy.hp -= damage;
				GD.Print("Hit registered");
			}
			arrow.Visible = false;
			QueueFree();
		}
	}
}
