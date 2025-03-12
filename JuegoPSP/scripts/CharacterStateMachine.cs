using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class CharacterStateMachine : Node
{
    [Export] public State currentState;
    [Export] CharacterBody2D character;
    [Export] AnimationTree animationTree;
    ArrayList states = new ArrayList();

    public override void _Ready()
    {
        foreach (Node child in GetChildren())
        {
            if (child is State state)
            {
                states.Add(child);
				state.character = character;
				state.playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

				child.Connect("interrupt_state", new Callable(this, nameof(on_state_interrupt_state)));

            } GD.PushWarning("Child " + child.Name + " is not a State");
        }
    }
	public override void _PhysicsProcess(Double delta){
		if (currentState.next_state != null)
		{
			switch_states(currentState.next_state);
		}
		currentState.state_process(delta);
	}

    public Boolean check_if_can_move(){
        return currentState.can_move;
    }

	public void switch_states(State new_state){
		if (currentState != null)
		{
			currentState.on_exit();
			currentState.next_state = null;
			
		}
		currentState = new_state;
		currentState.on_enter();
		
	}
	public override void _Input(InputEvent @event){
		currentState.state_input(@event);
	}

	public void on_state_interrupt_state(State new_state){
		switch_states(new_state);
	}
}
