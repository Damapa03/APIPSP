namespace TodoApi.Models;

public class Usuarios{
    
    public long Id {get; set;}
    public required string Username {get; set;}
    public required string Password {get; set;}
    public long Score {get; set;}

}