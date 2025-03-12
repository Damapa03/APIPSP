using Godot;
using System;
using System.Threading.Tasks;

public partial class Game : Node2D
{
	[Export]Label caracoles;
	[Export]Label username;
	

	public static int snails;
	private int Score;
	private string Usuario;
	public override void _Ready()
	{
		foreach (var child in GetChildren())
		{

			if (child is Snail snail)
			{
				snails += 1; 

				if(snails > 3) snails = 3;
			}
		}

		ApiClient apiClient = GetNode<ApiClient>("ApiClient");

		if (apiClient == null)
	{
		GD.PrintErr("Error: No se encontró el nodo Emisor.");
		return;
	}

		Error error = apiClient.Connect(nameof(ApiClient.MiSenal), new Callable(this, nameof(OnMiSenalRecibida)));
		 if (error != Error.Ok)
	{
		GD.PrintErr("Error al conectar la señal: " + error);
	}
	else
	{
		GD.Print("Señal conectada correctamente.");
		apiClient.EnviarSenal();
	}

	}

	private void OnMiSenalRecibida(string mensaje, int numero)
	{
		GD.Print($"Señal recibida en Receptor: {mensaje}, {numero}");
		username.Text = mensaje;
		Score = numero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		caracoles.Text = "X " + Score;

		if (snails == 0)
		{
			put();
			GetTree().ChangeSceneToFile("res://scenes/UI/Finish.tscn");
		}
	}

	private async Task put()
	{
		ApiClient apiClient = new ApiClient();
		await apiClient.PutScore(Score);

	}
}
