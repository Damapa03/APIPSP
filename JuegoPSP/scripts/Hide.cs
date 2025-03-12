using Godot;
using System;

public partial class Hide : State
{
    
    [Export] State return_state;
    [Export] String hide_animation_node = "hide";
    [Export] String reveal_animation_node = "reveal";

    Timer timer;

    public override void _Ready(){
        timer = GetNode<Timer>("Timer");
    }

    public override void on_enter(){
        playback.Travel(hide_animation_node);
        timer.Start();
    }
    
    public void _on_timer_timeout(){
        next_state = return_state;
        playback.Travel(reveal_animation_node);
    }
}
