using Godot;
using System;

public partial class world : Node3D
{
	public Node3D betaEnemy;
	public Node3D weaponSpot;
	public playerScript player;
	public int stageNumber = 2;
	Random rnd = new Random();
	public int enemyNum = 0;
	string[] aaaaa = new string[4];
	
	public void startStage()
	{
		for(int i = 0; i < stageNumber; i++)
		{
			var enemyScene = ResourceLoader.Load<PackedScene>("res://scenes/testEnemy.tscn");
			Node3D enemyInstance = enemyScene.Instantiate<Node3D>();
			enemyInstance.Position = new Vector3((float)rnd.Next(-11,20),2.4f,(float)rnd.Next(-15,17));
			AddChild(enemyInstance);
			enemyNum++;
		}
	}
	
	public void onEnemyDeath()
	{
		enemyNum -= 1;
		if(enemyNum == 0)
		{
			
			player.weaponPickups.Values.CopyTo(aaaaa,0);
			GD.Print("res://scenes/" + aaaaa[rnd.Next(0,3)] + "_pickUp.tscn");
			var pickupScene = ResourceLoader.Load<PackedScene>("res://scenes/" + aaaaa[rnd.Next(0,3)] + "_pickUp.tscn");
			GD.Print("res://scenes/" + aaaaa[rnd.Next(0,3)] + "_pickUp.tscn");
			Node3D pickupInstance = pickupScene.Instantiate<Node3D>();
			GD.Print("res://scenes/" + aaaaa[rnd.Next(0,3)] + "_pickUp.tscn");
			pickupInstance.GlobalPosition = weaponSpot.GlobalPosition;
			GD.Print("res://scenes/" + aaaaa[rnd.Next(0,3)] + "_pickUp.tscn");
			AddChild(pickupInstance);
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(player.currentHealth < 0)
		{
			GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://scenes/loadingScene.tscn").Instantiate());
			QueueFree();
		}

		
	}
	
	public override void _Ready()
	{
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		Vector2 windowSize = DisplayServer.WindowGetSize();
		weaponSpot = GetNode<Node3D>("Node3D");
		player = GetNode<playerScript>("player/CharacterBody3D");
	}
}
