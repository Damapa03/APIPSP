using Godot;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using HttpClient = System.Net.Http.HttpClient;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public partial class ApiClient : Node
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private string apiUrl = "https://apipsp.onrender.com";
    [Signal]
    public delegate void MiSenalEventHandler(string mensaje, int numero);
    private static string username;
    private static int id;
    private static int score;


    public async Task Login(string user, string password)
    {
        string logUrl = apiUrl + "/login";

        try
        {
            var json = "{\"username\":\"" + user + "\", \"password\":\"" + password + "\"}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(logUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string responseText = await response.Content.ReadAsStringAsync();
                GD.Print("Respuesta de la API:", responseText);

                JsonDocument doc = JsonDocument.Parse(responseText);
            
                JsonElement root = doc.RootElement;
                
                // Extraer username
                username = root.GetProperty("username").GetString();
                id = root.GetProperty("id").GetInt32();
                
                // Extraer score
                score = root.GetProperty("score").GetInt32();

            }
            else
            {
                GD.PrintErr("Error en la solicitud:", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr("Excepción:", ex.Message);
        }
    }
    public void EnviarSenal()
    {

    GD.Print($"EmitSignal con mensaje: {username}, número: {score}");
    EmitSignal(nameof(MiSenal), username, score);
    }
    public async Task Register(string user, string password)
    {
        string postUrl = apiUrl + "/register";
        try
        {
            var json = "{\"id\":0," + "\"username\":\"" + user + "\", \"password\":\"" + password + "\" , \"score\":10}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PostAsync(postUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string responseText = await response.Content.ReadAsStringAsync();
                GD.Print("Respuesta del POST:", responseText);
            }
            else
            {
                GD.PrintErr("Error en POST:", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr("Excepción en POST:", ex.Message);
        }
    }

    public async Task PutScore(int score)
    {
        string putUrl = apiUrl + "/api/usuarios/" + id;
        
        score += 3;

        try
        {
            var json = "{\"score\":"+score+"}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PutAsync(putUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string responseText = await response.Content.ReadAsStringAsync();
                GD.Print("Respuesta del PUT:", responseText);
            }
            else
            {
                GD.PrintErr("Error en PUT:", response.StatusCode);
            }
        }
        catch(Exception ex)
        {
            GD.PrintErr("Excepción en PUT:", ex.Message);
        }
    }
}
