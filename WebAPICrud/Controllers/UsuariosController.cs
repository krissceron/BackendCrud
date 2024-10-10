using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICrud.Data;
using WebAPICrud.Models;

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

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
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

        [HttpGet("{nombreusuario}/{contrasenia}")]
        public ActionResult<List<Usuario>> GetIniciarSesion(string nombreusuario, string contrasenia)
        {
            var usuario = _context.Usuarios.Where(usuario => usuario.UsuUsuario.Equals(nombreusuario) && usuario.UsuContrasenia.Equals(contrasenia)).ToList();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuId == id);
        }

        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<ActionResult<Usuario>> Register(Usuario usuario)
        {
            // Verificar si el nombre de usuario ya existe
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuUsuario == usuario.UsuUsuario);

            if (existingUser != null)
            {
                return Conflict("El nombre de usuario ya está registrado.");
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = usuario.UsuId }, usuario);
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Usuario>> Login(string nombreusuario, string contrasenia)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuUsuario == nombreusuario && u.UsuContrasenia == contrasenia);

            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(usuario);
        }
    }
}
