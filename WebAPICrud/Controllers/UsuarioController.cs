using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICrud.Models;
using Microsoft.EntityFrameworkCore;
using WebAPICrud.Data;

namespace WebAPICrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Contexto de Base de datos
        private readonly DbCrudContext dbContext;
        //inyección de dependencia
        public UsuarioController(DbCrudContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult> GetListaUsuarios()
        {
            var listaUsuarios = await dbContext.Usuarios.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaUsuarios);
        }

        [HttpGet]
        [Route("Obtener/{id}")]
        public async Task<ActionResult> GetUsuario(int id)
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.UsuId==id);
            return StatusCode(StatusCodes.Status200OK, usuario);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<ActionResult> NuevoUsuario([FromBody] Usuario objeto)
        {
            await dbContext.Usuarios.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new {mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<ActionResult> EditarUsuario([FromBody] Usuario objeto)
        {
            dbContext.Usuarios.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.UsuId == id);
            dbContext.Usuarios.Remove(usuario);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}
