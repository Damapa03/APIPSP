using Godot;
using System;

public partial class KillZone : Area2D
{
	Damageable damageable;
	public void _on_body_entered(Node2D body)
	{
		Engine.TimeScale = 0.5;
		body.GetNode("CollisionShape2D").QueueFree();
		Timer timer = GetNode<Timer>("Timer");
		timer.Start();
	}

	public void _on_timer_timeout()
	{
		Engine.TimeScale = 1;
		GetTree().ChangeSceneToFile("res://scenes/UI/GameOver.tscn");
	}
}
