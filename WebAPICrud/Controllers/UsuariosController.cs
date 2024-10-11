using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICrud.Data;
using WebAPICrud.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;

namespace WebAPICrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbCrudContext _context;

        public UsuariosController(DbCrudContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios/Lista
        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/Obtener/5
        [HttpGet("Obtener/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuarios/Nuevo
        [HttpPost("Nuevo")]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Encriptar la contraseña antes de guardar
            usuario.UsuContrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.UsuContrasenia);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuId }, usuario);
        }

        // PUT: api/Usuarios/Editar/5
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/Eliminar/5
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Usuarios/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel usuario)
        {
            var user = await _context.Usuarios
                .Where(u => u.UsuUsuario == usuario.UsuUsuario)
            .FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(usuario.UsuContrasenia, user.UsuContrasenia))
            {
                return Unauthorized(new { message = "Credenciales incorrectas." });
            }

            return Ok(user); // Aquí puedes devolver un token en lugar de los datos del usuario
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuId == id);
        }
    }
}