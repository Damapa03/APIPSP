using Godot;
using System;
using System.Resources;

public partial class HitState : State
{
	[Export] Damageable damageable;
    [Export] CharacterStateMachine characterStateMachine;
    [Export] State dead_state;
    [Export] String dead_animation_node = "death";
    [Export] float knockback_speed = 100;
    [Export] State hide_state;   
 
    public override void _Ready() 
    {
       damageable.Connect("on_hit", new Callable(this, nameof(on_damageable_hit)));
    }

    public override void on_exit()
    {
        character.Velocity = Vector2.Zero;
    }


    private void on_damageable_hit(Node node, Vector2 knockbackDirection){
        if(damageable.Health > 0)
        {
            character.Velocity = knockback_speed * knockbackDirection;
            EmitSignal("interrupt_state", hide_state);
        }else{
             
                EmitSignal("interrupt_state", dead_state);
                playback.Travel(dead_animation_node);
             }

	}
}
