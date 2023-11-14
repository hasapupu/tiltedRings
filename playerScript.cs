using Godot;
using System;

public partial class playerScript : CharacterBody3D
{
	public float speed = 5f;
	const float gravity = 15f;
	public float jumpForce = 10;
	const float acceleration = 0.5f;
	const float sens = 0.01f;
	public Camera3D cam;
	public Node3D head;
	private Vector3 velocity = Vector3.Zero;
	
	public override void _Ready()
	{
		head = GetNode<Node3D>("Head");
		cam = GetNode<Camera3D>("Head/Camera3D"); 
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
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Vector3 direction = (head.GlobalTransform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();
		
		if(direction != Vector3.Zero)
		{
			velocity.X = direction.X * speed;
			velocity.Z = direction.Z * speed;
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
		
		if(IsOnFloor() && Input.IsActionJustPressed("jump"))
		{
			velocity.Y = jumpForce;
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
