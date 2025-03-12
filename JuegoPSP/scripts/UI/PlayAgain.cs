using Godot;
using System;

public partial class PlayAgain : Button
{
	public void _on_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
}
