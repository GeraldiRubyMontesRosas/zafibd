using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetario_Api.DTOs;
using Recetario_Api.Entities;

namespace Recetario_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuarioController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            var usuarios = await context.Usuarios.ToListAsync();
            if (!usuarios.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<UsuarioDTO>>(usuarios));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuario = mapper.Map<Usuario>(dto);
            context.Add(usuario);
            try
            {

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar al usuario.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el Id del objeto no coinciden");
            }
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            mapper.Map(dto, usuario);
            context.Update(usuario);
            try
            {
                await context.SaveChangesAsync();
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
        private bool UsuarioExists(int id)
        {
            return context.Usuarios.Any(e => e.Id == id);
        }


    }

}