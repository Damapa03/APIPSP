using Godot;
using System;

public partial class PlayerDamageable : Node
{
    signal_bus SignalBus;
    [Signal] public delegate void on_hit_playerEventHandler(Node node, int damage_taken, Vector2 knockbackDirection);
    [Export] private float health = 20;
    [Export] CharacterStateMachine state;
    [Export] PlayerHitState hit_state;
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
    public virtual void hit(int damage, Vector2 knockbackDirection)
    {
        health -= damage;
        progressBar.Value = health;
        hit_state.on_player_damageable_hit(GetParent(), knockbackDirection);
        
    }
}
