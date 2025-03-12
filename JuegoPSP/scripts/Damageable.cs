using Godot;
using System;


public partial class Damageable : Node
{
    signal_bus SignalBus;
    [Signal] public delegate void on_hitEventHandler(Node node, int damage_taken, Vector2 knockbackDirection);
    [Export] private float health = 20;
    [Export]private String dead_animation_name = "death";
    [Export] CharacterStateMachine state;
    [Export] State hide_state;
    [Export] ProgressBar progressBar;


    public float Health
    {
        get { return health; }
        set
        {
            SignalBus.EmitSignal("on_health_changed", GetParent(), value - health);
            health = value;
        }   
    }

    public override void _Ready()
    {
        SignalBus = GetNode<signal_bus>("/root/SignalBus");
        progressBar.MaxValue = health;
        progressBar.Value = health;
    }
    public virtual void hit(int damage, Vector2 knockbackDirection){
        
        if (state.currentState != hide_state)
        {
            health -= damage;
            progressBar.Value = health;
        }

        EmitSignal("on_hit", GetParent(), knockbackDirection);
    }

    public void _on_animation_tree_animation_finished(String anim_name){
        if (anim_name == dead_animation_name)
        {
            GetParent().QueueFree();
            Game.snails -= 1;
        }
    }

}
