using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class UsuarioContext : DbContext
{
    public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuario { get; set; } = null!;
}