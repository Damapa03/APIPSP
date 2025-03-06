using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Models.DTO;

namespace TodoApi.Controllers;

[Route("api/usuarios")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioContext _context;

    public UsuarioController(UsuarioContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
    {
        return await _context.Usuario
            .Select(x => UsuarioDTO(x))
            .ToListAsync();
    }

    // GET: api/Usuario/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDTO>> GetUsuario(long id)
    {
        var usuario = await _context.Usuario.FindAsync(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return UsuarioDTO(usuario);
    }
    // </snippet_GetByID>

    [HttpPost("/login")]
    public async Task<ActionResult<UsuarioDataDTO>> LogUsuario(UsuarioLogDTO usuarioLogDTO)
    {
        long id = 0;
        var usuarios = await GetUsuarios();

        if(usuarios != null){
            foreach(var u in usuarios.Value){
                if(u.Username == usuarioLogDTO.Username){
                    id = u.Id;
                }
            }
        }

        var usuario = await _context.Usuario.FindAsync(id);

        if (usuario == null)
        {
            return NotFound();
        }

        if (usuario.Username == usuarioLogDTO.Username && usuario.Password == usuarioLogDTO.password)
        {
            UsuarioDataDTO usuarioDataDTO = new()
            {
                Id = id,
                Username = usuario.Username,
                Score = usuario.Score
            };


            return usuarioDataDTO;
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, UsuarioPutDTO usuarioDTO)
    {
        
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        usuario.Score = usuarioDTO.Score;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!UsuarioExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Usuario
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost("/register")]
    public async Task<ActionResult<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDTO)
    {
        var usuario = new Usuario
        {
            Username = usuarioDTO.Username,
            Password = usuarioDTO.password,
            Score = usuarioDTO.Score
        };

        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetUsuario),
            new { id = usuario.Id },
            UsuarioDTO(usuario));
    }
    // </snippet_Create>

    // DELETE: api/Usuario/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(long id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.Usuario.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UsuarioExists(long id)
    {
        return _context.Usuario.Any(e => e.Id == id);
    }

    private static UsuarioDTO UsuarioDTO(Usuario usuario) =>
       new UsuarioDTO
       {
           Id = usuario.Id,
           Username = usuario.Username!,
           password = usuario.Password!,
           Score = usuario.Score
       };
}