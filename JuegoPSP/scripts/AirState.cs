using Godot;
using System;

public partial class AirState : State
{
    [Export] State landing_state;
    [Export] float double_jump_velocity = -100.0f;
    [Export] String double_jump_animation = "double_jump";
    [Export] String landing_animation = "landing";
    bool has_double_jumped = false;
    

    public override void state_process(double delta){
        if (character.IsOnFloor())
        {
            next_state = landing_state;
        }
    }

    public override void on_exit()
    {
        if (next_state == landing_state)
        {
            playback.Travel(landing_animation);
            has_double_jumped = false;
        }
    }

    public override void state_input(InputEvent @event)
    {
        if (@event.IsActionPressed("jump") && !has_double_jumped)
        {
            double_jump();
        }
    }

    public void double_jump()
    {
        character.Velocity = new Vector2(character.Velocity.X, double_jump_velocity);
        playback.Travel(double_jump_animation);
        has_double_jumped = true;
    }
}
