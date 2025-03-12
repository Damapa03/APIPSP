using Godot;
using System;
using System.Threading.Tasks;

public partial class LoginButton : Button
{
	[Export] LineEdit username;
	[Export] LineEdit password;

	public void _on_pressed()
	{

		GD.Print("login");
		login();

	}
	private async Task login()
	{
		if (username.Text != "" && password.Text != "")
		{
				ApiClient apiClient = new ApiClient();
				await apiClient.Login(username.Text, password.Text);

			GetTree().ChangeSceneToFile("res://scenes/game.tscn");

		}
	}
}
