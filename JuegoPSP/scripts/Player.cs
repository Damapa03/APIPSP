using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 200.0f;

	[Signal]public delegate void facing_direction_changedEventHandler(bool right_position);

	AnimationTree animationTree;
	Sprite2D sprite2D;
	CharacterStateMachine stateMachine;
	public override void _Ready()
	{
		sprite2D = GetNode<Sprite2D>("Sprite2D");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationTree.Active = true;
		stateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", null, null);
		if (direction != Vector2.Zero && stateMachine.check_if_can_move())
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		//Change direction

		if (direction.X > 0)
		{
			sprite2D.FlipH = false;
		}else if (direction.X < 0)
		{
			 sprite2D.FlipH = true;
		}
		EmitSignal("facing_direction_changed", !sprite2D.FlipH);


		//Change animation
		animationTree.Set("parameters/move/blend_position", direction.X);
		

		Velocity = velocity;
		MoveAndSlide();
	}
}