using Godot;
using System;

public partial class GroundState : State
{
    [Export] public float JumpVelocity = -400.0f;
    [Export] State air_state;
    [Export] String jump_animation = "jump";
    [Export] State attack_state;
    [Export] private String attack_animation = "attack1";

    private Timer timer;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
    }

    public override void state_input(InputEvent @event){
        if (@event.IsActionPressed("jump"))
        {  
            Jump();
        }

        if (@event.IsActionPressed("attack"))
        {
            attack();
        }
    }
    public override void state_process(double delta){
        if (!character.IsOnFloor() && timer.IsStopped())
        {
            next_state = air_state;
        }
    }
    public void Jump()
    {
        character.Velocity = new Vector2(character.Velocity.X, JumpVelocity);
        next_state = air_state;
        playback.Travel(jump_animation);
    }
    public void attack(){
        next_state = attack_state;
        playback.Travel(attack_animation);
    }
}
