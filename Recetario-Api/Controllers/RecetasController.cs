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
    public class RecetasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RecetasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<RecetasDTO>>> GetAll()
        {
            var recetas = await context.Receta
                .Include(r => r.PasosL)
                .Include(r => r.Ingredientes)
                    .ThenInclude(i => i.Unidad)
                .ToListAsync();
            if (!recetas.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<RecetasDTO>>(recetas));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] RecetasDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var receta = mapper.Map<Receta>(dto);
            context.Add(receta);
            try
            {

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message,
                    FullError = ex.ToString()
                });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var receta = await context.Receta.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }
            context.Receta.Remove(receta);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{Recetaid:int}")]
        public async Task<ActionResult> Put(int Recetaid, [FromBody] RecetasDTO dto)
        {
            if (Recetaid != dto.Recetaid)
            {
                return BadRequest("El ID de la ruta y el Id del objeto no coinciden");
            }
            var receta = await context.Receta.FindAsync(Recetaid);
            if (receta == null)
            {
                return NotFound();
            }
            mapper.Map(dto, receta);
            context.Update(receta);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaExiste(Recetaid))
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
        private bool RecetaExiste(int Recetaid)
        {
            return context.Receta.Any(e => e.RecetaId == Recetaid);
        }
    }
}
