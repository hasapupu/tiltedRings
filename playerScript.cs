using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public enum weaponState {NONE, LONGSWORD, SHORTSWORD, BOW, CROSSBOW, SCEMITAR}
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
	Area3D SS_hitBox;
	Area3D SC_hitBox;
	weaponState prevState = weaponState.NONE;
	bool isBWAnim = false;
	bool isCBAnim = false;
	RayCast3D BW_ray;
	RayCast3D CB_ray;
	public Dictionary<weaponState,string> weaponPickups = new Dictionary<weaponState,string>() {{weaponState.LONGSWORD, "LS"},{weaponState.SHORTSWORD,"SS"},{weaponState.CROSSBOW,"CB"},{weaponState.SCEMITAR, "SC"}};
	
	public override void _Ready()
	{
		head = GetNode<Node3D>("Head");
		cam = GetNode<Camera3D>("Head/Camera3D"); 
		scaleRod = GetNode<Node2D>("Head/Node2D/Gitjam-scaletop");
		GD.Print(scaleRod);
		hitBox = GetNode<Area3D>("Head/Camera3D/Node3D/Longsword/Longsword/MeshInstance3D/hitbox");
		SS_hitBox = GetNode<Area3D>("Head/Camera3D/Node3D2/Shortsword/Shortsword/MeshInstance3D/hitbox");
		SC_hitBox = GetNode<Area3D>("Head/Camera3D/Node3D3/Scemitar/Scemitar/MeshInstance3D/hitbox");
		healthDisplay = GetNode<Label>("Head/Control/Label");
		CB_ray = GetNode<RayCast3D>("Head/Camera3D/Node3D4/Crossbow/RayCast3D");
		weapons.Add(GetNode<Node3D>("Head/Camera3D/Node3D/Longsword"));
		weapons.Add(GetNode<Node3D>("Head/Camera3D/Node3D2/Shortsword"));
		weapons.Add(GetNode<Node3D>("Head/Camera3D/Node3D3/Scemitar"));
		weapons.Add(GetNode<Node3D>("Head/Camera3D/Node3D4/Crossbow"));
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
		prevState = state;
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
					if(weapon.IsInGroup("Longsword"))
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
				animPlayer.Play("LS_idle");
				break;
			
			case weaponState.SHORTSWORD:
				foreach(Node3D weapon in weapons)
				{
					if(weapon.IsInGroup("Shortsword"))
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
				animPlayer.Play("SS_idle");
				break;
			case weaponState.SCEMITAR:
				foreach(Node3D weapon in weapons)
				{
					if(weapon.IsInGroup("Scemitar"))
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
				animPlayer.Play("SC_idle");
				break;
			case weaponState.CROSSBOW:
				foreach(Node3D weapon in weapons)
				{
					if(weapon.IsInGroup("Crossbow"))
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
				animPlayer.Play("CB_idle");
				break;
			case weaponState.BOW:
				foreach(Node3D weapon in weapons)
				{
					if(weapon.IsInGroup("Bow"))
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
		/*if(prevState != weaponState.NONE)
		{
			var pickupScene = ResourceLoader.Load<PackedScene>("res://scenes/" + weaponPickups[prevState] + "_pickUp.tscn");
			Node3D pickupInstance = pickupScene.Instantiate<Node3D>();
			GetParent().GetParent().AddChild(pickupInstance);
			pickupInstance.Position = new Vector3(GlobalPosition.X + Math.Abs(head.Rotation.Y),0.1f,GlobalPosition.Z + (head.Rotation.Y));
			GD.Print(head.Rotation);
		}
		GD.Print(state);*/
	}
	
	public override void _Process(double delta)
	{
		//setWeaponState(state);
		if(scaleValue < 3)
		{
			scaleRod.Rotation = (scaleValue - 1) / 4;
		}
		healthDisplay.Text = maxHealth+"/"+currentHealth;
		//will have to change based on weapon and cd
		if(Input.IsActionJustPressed("attack"))
		{
			LS_hitbox hitScript;
			GD.Print("attack");
			switch(state)
			{
				case weaponState.LONGSWORD:
					animPlayer.Play("LS_swing");
					hitBox.Monitoring = true;
					hitScript = hitBox as LS_hitbox;
					hitScript.damage = 40 * scaleValue;
					scaleValue -= (float)((scaleValue / 100) * 15);
					currentHealth += (int)Mathf.Floor(1/scaleValue);
					break;
				
				case weaponState.SHORTSWORD:
					animPlayer.Play("SC_swing");
					SS_hitBox.Monitoring = true;
					hitScript = SS_hitBox as LS_hitbox;
					hitScript.damage = 20 * scaleValue;
					scaleValue -= (float)((scaleValue / 100) * 5);
					currentHealth += (int)Mathf.Floor(1f/scaleValue);
					break;
					
				case weaponState.SCEMITAR:
					animPlayer.Play("SS_swing");
					SC_hitBox.Monitoring = true;
					hitScript = hitBox as LS_hitbox;
					hitScript.damage = 30 * scaleValue;
					scaleValue -= (float)((scaleValue / 100) * 10); 
					currentHealth += (int)Mathf.Floor(1/scaleValue);
					break;
					
				case weaponState.CROSSBOW:
					if(!isCBAnim)
					{
						currentHealth += (int)Mathf.Floor(1/scaleValue);
						scaleValue -= (float)((scaleValue / 100) * 10); 
						animPlayer.Play("CB_shoot");
						isCBAnim = true;
						var arrow = ResourceLoader.Load<PackedScene>("res://scenes/arrow.tscn");
						Node3D arrowInstance = arrow.Instantiate<Node3D>();
						arrowInstance.Position = GetNode<Node3D>("Head/Camera3D").GlobalPosition - new Vector3(1,0,0);
						arrowInstance.Rotation = new Vector3(GetNode<Node3D>("Head/Camera3D/Node3D4/Crossbow").GlobalRotation.X,GetNode<Node3D>("Head/Camera3D/Node3D4/Crossbow").GlobalRotation.Y,GetNode<Node3D>("Head/Camera3D/Node3D4/Crossbow").GlobalRotation.Z);
						var instanceScript = arrowInstance as arrowScript;
						instanceScript.damage = 30 * scaleValue;
						GetParent().GetParent().AddChild(arrowInstance);
					}
					break;

				case weaponState.BOW:
					if(!isBWAnim)
					{
						scaleValue -= (float)((scaleValue / 100) * 30); 
						animPlayer.Play("BW_shoot");
						isCBAnim = true;
						var arrow = ResourceLoader.Load<PackedScene>("res://scenes/arrow.tscn");
						Node3D arrowInstance = arrow.Instantiate<Node3D>();
						arrowInstance.Position = GetNode<Node3D>("Head/Camera3D/Node3D4/Crossbow").GlobalPosition;
						arrowInstance.Rotation = new Vector3(GetNode<Node3D>("Head/Camera3D/Node3D5/Bow").GlobalRotation.X,GetNode<Node3D>("Head/Camera3D/Node3D5/Bow").GlobalRotation.Y,GetNode<Node3D>("Head/Camera3D/Node3D5/Bow").GlobalRotation.Z);
						GetParent().GetParent().AddChild(arrowInstance);
					}
					break;
			}
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
			hitBox.Monitoring = false;
		}
		else if((string)anim_name == "SC_swing")
		{	
			animPlayer.Play("SS_idle");
			SS_hitBox.Monitoring = false;
		}
		else if((string)anim_name == "SS_swing")
		{	
			animPlayer.Play("SC_idle");
			SC_hitBox.Monitoring = false;
		}
		else if((string)anim_name == "CB_shoot")
		{	
			animPlayer.Play("CB_idle");
			isCBAnim = false;
		}
		else if((string)anim_name == "BW_shoot")
		{	
			animPlayer.Play("BW_idle");
			isBWAnim = false;
		}
	}
}



