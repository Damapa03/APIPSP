using Godot;
using System;

public partial class State : Node
{
    [Export] public Boolean can_move = true;

    [Signal] public delegate void interrupt_stateEventHandler(State new_state);

    public CharacterBody2D character;
    public State next_state;
    public AnimationNodeStateMachinePlayback playback;

    public virtual void state_process(double delta){}
    public virtual void state_input(InputEvent @event){}

    public virtual void on_enter() { }

    public virtual void on_exit() { }
}
