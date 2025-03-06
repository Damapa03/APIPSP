namespace TodoApi.Models.DTO;

public class UsuarioDataDTO
{  
    public long Id {get; set;}
    public required string Username {get; set;}
    public long Score {get; set;}
}