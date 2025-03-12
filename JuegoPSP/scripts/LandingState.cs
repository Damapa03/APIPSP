using Godot;
using System;

public partial class LandingState : State
{
    [Export] private String landing_animation = "landing";
    [Export] private State ground_state;
    public void _on_animation_tree_animation_finished(string anim)
    {
        if (anim == landing_animation)
        {
            next_state = ground_state;
        }
    }
}
