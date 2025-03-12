using Godot;
using System;

public partial class PlayerHit : State
{
    [Export] PlayerDamageable damageable;
    [Export] CharacterStateMachine characterStateMachine;
    [Export] State dead_state;
    [Export] float knockback_speed = 100;
 
    public override void _Ready() 
    {
       damageable.Connect("hit_player", new Callable(this, nameof(on_player_damageable_hit)));

    }

    public override void on_exit()
    {
        character.Velocity = Vector2.Zero;
    }


    private void on_player_damageable_hit(Node node, Vector2 knockbackDirection){
        if(damageable.Health > 0)
        {
            character.Velocity = knockback_speed * knockbackDirection;
            EmitSignal("interrupt_state", this);
        }else{
                EmitSignal("interrupt_state", dead_state);
             }

	}
}
