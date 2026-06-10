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
    public class UnidadController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UnidadController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<UnidadDTO>>> GetAll()
        {
            var unidad = await context.Unidad.ToListAsync();
            if (!unidad.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<UnidadDTO>>(unidad));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] UnidadDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var unidad = mapper.Map<Unidad>(dto);
            context.Add(unidad);
            try
            {

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar la unidad.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{IdUnidad:int}")]
        public async Task<ActionResult> Delete(int IdUnidad)
        {
            var unidad = await context.Unidad.FindAsync(IdUnidad);
            if (unidad == null)
            {
                return NotFound();
            }
            context.Unidad.Remove(unidad);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar")]
        public async Task<ActionResult> Put(int IdUnidad, [FromBody] UnidadDTO dto)

        {
            if (IdUnidad != dto.UnidadId)
            {
                return BadRequest("El ID de la ruta y el Id del objeto no coinciden");
            }
            var unidad = await context.Unidad.FindAsync(IdUnidad);
            if (unidad == null)
            {
                return NotFound();
            }
            mapper.Map(dto, unidad);
            context.Update(unidad);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadExists(IdUnidad))
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
        private bool UnidadExists(int IdUnidad)
        {
            return context.Unidad.Any(e => e.UnidadId == IdUnidad);
        }
    }
}
