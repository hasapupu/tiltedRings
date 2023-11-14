using Godot;
using System;
using System.Collections;

//public enum weaponState
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
	
	public override void _Ready()
	{
		head = GetNode<Node3D>("Head");
		cam = GetNode<Camera3D>("Head/Camera3D"); 
		scaleRod = GetNode<Node2D>("Head/Node2D/Gitjam-scaletop");
		GD.Print(scaleRod);
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
	
	public override void _Process(double delta)
	{
		if(scaleValue < 2)
		{
			scaleRod.Rotation = (scaleValue - 1) / 4;
		}
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GD.Print(scaleValue);
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
			velocity.Y = jumpForce;
			scaleValue += 0.005f; 
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}