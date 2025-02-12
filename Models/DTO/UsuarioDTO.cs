namespace TodoApi.Models.DTO;

public class UsuarioDTO
{  
    public long Id {get; set;}
    public required string Username {get; set;}
    public required string password {get; set;}
    public long Score {get; set;}

}