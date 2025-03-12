using Godot;
using System;

public partial class Exit : Button
{
	public void _on_pressed()
	{
		GetTree().Quit();
	}
}
