 using Godot;
using System;

public partial class HealthManager : Control
{
	// public override void _Ready()
	// {
	// 	signal_bus.Connect("on_health_changed", on_signal_health_changed);
	// }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void on_signal_health_changed(Node node, int damage)
	{
		
	}
}
