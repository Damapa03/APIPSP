using Godot;
using System;

public partial class PlayerHitState : State
{
    [Export] PlayerDamageable damageable;
    [Export] CharacterStateMachine characterStateMachine;
    [Export] State dead_state;
    [Export] float knockback_speed = 100;

    public override void _Ready() 
    {
       damageable.Connect("on_hit_player", new Callable(this, nameof(on_player_damageable_hit)));

    }
    public override void on_enter()
    {
        characterStateMachine.currentState = this;
        
    }

    public override void on_exit()
    {
        character.Velocity = Vector2.Zero;
    }
    
    public void on_player_damageable_hit(Node node, Vector2 knockbackDirection){
        if(damageable.Health > 0)
        {
            character.Velocity = knockback_speed * knockbackDirection;
            characterStateMachine.switch_states(this);
        }else{
                characterStateMachine.switch_states(dead_state);
             }

	}
}
