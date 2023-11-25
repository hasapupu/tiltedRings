using Godot;
using System;

public partial class enemyMovementBaseClas : CharacterBody3D
{
	public int speed = 5;
	public int gravity = 15;
	public Node3D player;
	public bool isHit = false;
	
	public override void _Ready()
	{
		player = GetParent().GetParent().GetNode<Node3D>("player/CharacterBody3D");
		GD.Print(player);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 targetDirection = (player.GlobalTransform.Origin - GlobalTransform.Origin).Normalized();
		if(!isHit)
			Velocity = new Vector3(targetDirection.X * speed, Velocity.Y,targetDirection.Z * speed);
		
		if(!IsOnFloor())
		{
			Velocity = new Vector3(Velocity.X,Velocity.Y - (gravity * (float)delta), Velocity.Z);
		}
		
		GD.Print();
		MoveAndSlide();
	}
}
