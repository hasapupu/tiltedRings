using Godot;
using System;

public partial class enemyHitboxBaseClass : Area3D
{
	public float hp = 100f;
	public int damage = 30;
	
	public override void _PhysicsProcess(double delta)
	{
		if(hp <= 0f)
		{
			var zaWarudo = GetParent().GetParent().GetParent() as world;
			zaWarudo.onEnemyDeath();
			GetParent().GetParent().QueueFree();
			
		}
	}
}
