using Godot;
using System;

public partial class  FacingCollisionShape2D : CollisionShape2D
{
	[Export]public Vector2 left_position;
	[Export]public Vector2 right_position;
}
