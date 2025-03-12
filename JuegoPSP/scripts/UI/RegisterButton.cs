using Godot;
using System;
using System.Threading.Tasks;

public partial class RegisterButton : Godot.Button
{
	[Export] LineEdit username;
	[Export] LineEdit password;

	public void _on_pressed()
	{
		GD.Print("register");
		register();
	}

	private async Task register()
	{
		if (username.Text != "" && password.Text != "")
		{
			GD.Print("entra");
			ApiClient apiClient = new ApiClient();
			await apiClient.Register(username.Text, password.Text);
			username.Text = "";
			password.Text = "";
		}else GD.Print("no entra");
	}
}
