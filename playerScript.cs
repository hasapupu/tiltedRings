using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public enum weaponState {NONE, LONGSWORD}
public partial class playerScript : CharacterBody3D
{
	public float speed = 8f;
	const float gravity = 15f;
	public float jumpForce = 10;
	const float acceleration = 0.5f;
	const float sens = 0.01f;
	public Camera3D cam;
	public Node3D head;
	private Vector3 velocity = Vector3.Zero;
	public float scaleValue = 1f;
	public Node2D scaleRod;
	public int maxHealth = 100;
	public int currentHealth = 100;
	public Label healthDisplay;
	public weaponState state = weaponState.NONE;
	public List<Node3D> weapons = new List<Node3D>();
	AnimationPlayer animPlayer;
	Area3D hitBox;
	
	public override void _Ready()
	{
		head = GetNode<Node3D>("Head");
		cam = GetNode<Camera3D>("Head/Camera3D"); 
		scaleRod = GetNode<Node2D>("Head/Node2D/Gitjam-scaletop");
		GD.Print(scaleRod);
		hitBox = GetNode<Area3D>("Head/Camera3D/Node3D/Longsword/Cube/hitbox");
		healthDisplay = GetNode<Label>("Head/Control/Label");
		weapons.Add(GetNode<Node3D>("Head/Camera3D/Node3D/Longsword"));
		animPlayer = GetNode<AnimationPlayer>("Head/AnimationPlayer");
		setWeaponState(weaponState.NONE);
	}
	
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion)
		{
			InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
			head.RotateY(-mouseMotion.Relative.X * sens);
			cam.RotateX(-mouseMotion.Relative.Y * sens);
			Vector3 camRot = cam.Rotation;
			camRot.X = Mathf.Clamp(camRot.X, Mathf.DegToRad(-80), Mathf.DegToRad(80));
			cam.Rotation = camRot;
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}
	
	public void setWeaponState(weaponState updatedState)
	{
		
		state = updatedState;
		switch(updatedState)
		{
			default:
				foreach(Node3D weapon in weapons)
				{
					weapon.ProcessMode = ProcessModeEnum.Disabled;
					weapon.Hide();
				}
				break;
				
			case weaponState.LONGSWORD:
				foreach(Node3D weapon in weapons)
				{
					if(weapon is Longsword)
					{
						GD.Print("mogus");
						weapon.ProcessMode = ProcessModeEnum.Inherit;
						weapon.Show();	
						
					}
					else
					{
						weapon.ProcessMode = ProcessModeEnum.Disabled;
						weapon.Hide();
					}
				}
				break;
		}
		
	}
	
	public override void _Process(double delta)
	{
		//setWeaponState(state);
		if(scaleValue < 2)
		{
			scaleRod.Rotation = (scaleValue - 1) / 4;
		}
		healthDisplay.Text = maxHealth+"/"+currentHealth;
		//will have to change based on weapon and cd
		if(Input.IsActionJustPressed("attack"))
		{
			GD.Print("attack");
			animPlayer.Play("LS_swing");
			hitBox.Monitoring = true;
			scaleValue -= (float)((scaleValue / 100) * 15); 
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		//GD.Print(scaleValue);
		if(scaleValue > 1)
		{
			scaleValue -= 0.001f;
		}
		else if(scaleValue < 1)
		{
			scaleValue += 0.001f;
		}
		
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Vector3 direction = (head.GlobalTransform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();
		
		if(direction != Vector3.Zero)
		{
			velocity.X = direction.X * speed * (scaleValue);
			velocity.Z = direction.Z * speed * (scaleValue);
			scaleValue += 0.002f;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X,0, acceleration);
			velocity.Z = Mathf.MoveToward(Velocity.Z,0, acceleration);
		}
		
		if(!IsOnFloor())
		{
			velocity.Y -= gravity * (float)delta;
		}
		
		if(IsOnFloor() && Input.IsActionPressed("jump"))
		{
			velocity.Y = jumpForce * scaleValue;
			scaleValue += 0.005f; 
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
	
	void _on_animation_player_animation_finished(StringName anim_name)
	{	
		// Replace with function body.
		if((string)anim_name == "LS_swing")
		{	
			animPlayer.Play("LS_idle");
			hitBox.Monitoring = true;
		}
	}
}



