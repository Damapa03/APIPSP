using Godot;
using System;

public partial class Attack : State
{

	[Export] private State return_state;
	[Export] String attack1_name = "attack1";
	[Export] String attack2_name = "attack2";
	[Export] private String attack2_node = "attack2";
	[Export] private String return_animation_node = "move";
	
	private Timer timer;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
	}
	
	public override void state_input(InputEvent @event)
	{
		if (@event.IsActionPressed("attack"))
		{
			timer.Start();
		}
	}
	public void _on_animation_tree_animation_finished(string anim)
	{
		if (anim == attack1_name || anim == attack2_name)
		{
			if (timer.IsStopped())
			{
				next_state = return_state;
				playback.Travel(return_animation_node);
			}else playback.Travel(attack2_node);
		}

		if (anim == attack2_name)
		{
			next_state = return_state;
			playback.Travel(return_animation_node);
		}
	}
}
