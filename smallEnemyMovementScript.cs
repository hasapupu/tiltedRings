using Godot;
using System;

public partial class smallEnemyMovementScript : enemyMovementBaseClas
{
	AnimationPlayer anim;
	bool isDiving = false;
	

	private void _on_area_3d_body_entered(Node3D body)
	{
		GD.Print(isDiving);
		if(body.IsInGroup("player"))
		{
			var pluh = player as playerScript;
			pluh.currentHealth -= 20;
		}
	}

	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("Node3D/AnimationPlayer");
		player = GetParent().GetParent().GetNode<Node3D>("player/CharacterBody3D");
	}
	
	public override void inheritedBody()
	{
		if(Math.Abs(player.GlobalPosition.DistanceTo(GlobalPosition)) < 2.5f /*&& isDiving == false*/)
		{
			isDiving = true;
			anim.Play("charge");
			GD.Print("aaaaaaaaaaaaaaaaaaaaaa");
		}
	}
	
	private void _on_animation_player_animation_finished(StringName anim_name)
	{
		if((string)anim_name == "charge2")
		{
			anim.Play("charge2");
		}
		if((string)anim_name == "charge")
		{
			anim.Play("charge2");
			isDiving = false;
		}
		GD.Print("EggNog");
	}
}
