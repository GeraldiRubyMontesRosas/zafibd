using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetario_Api.DTOs;
using Recetario_Api.Entities;

namespace Recetario_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredienteRecetaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IngredienteRecetaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<IngredienteRecetaDTO>>> GetAll()
        {
            var IngredienteReceta = await context.IngredienteReceta
                 .Include(i => i.Unidad)
                 .Include(i => i.Ingrediente)
                 .ToListAsync();
            if (!IngredienteReceta.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<IngredienteRecetaDTO>>(IngredienteReceta));
        }
        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] IngredienteRecetaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IngredienteReceta = mapper.Map<IngredienteReceta>(dto);
            context.Add(IngredienteReceta);
            var recetaExiste = await context.Receta
    .       AnyAsync(r => r.RecetaId == dto.Recetaid);

            if (!recetaExiste)
            {
                return BadRequest("La receta no existe");
            }

            var ingredienteExiste = await context.Ingrediente
                .AnyAsync(i => i.IngredienteId == dto.IngredienteId);

            if (!ingredienteExiste)
            {
                return BadRequest("El ingrediente no existe");
            }
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

        [HttpPut("actualizar/{IngredienteRecetaId:int}")]
        public async Task<ActionResult> Put(int IngredienteRecetaId, [FromBody] IngredienteRecetaDTO dto)
        {
            if (IngredienteRecetaId != dto.IngredienteRecetaId)
            {
                return BadRequest("El Id de la ruta y el id del objeto no coinciden");
            }
            var IngredienteReceta = await context.IngredienteReceta.FindAsync(IngredienteRecetaId);
            if (IngredienteReceta == null)
            {
                return NotFound();
            }
            mapper.Map(dto, IngredienteReceta);
            context.Update(IngredienteReceta);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(IngredienteRecetaId))
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
        private bool IngredienteExists(int IngredienteRecetaId)
        {
            return context.IngredienteReceta.Any(e => e.IngredienteRecetaId == IngredienteRecetaId);
        }

        [HttpDelete("eliminar/{IngredienteRecetaId}")]
        public async Task<ActionResult> Delete(int IngredienteRecetaId)
        {
            var IngredienteReceta = await context.IngredienteReceta.FindAsync(IngredienteRecetaId);
            if (IngredienteReceta == null)
            {
                return NotFound();
            }
            context.IngredienteReceta.Remove(IngredienteReceta);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
