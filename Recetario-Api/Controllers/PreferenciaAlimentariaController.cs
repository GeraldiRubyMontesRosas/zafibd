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
    public class PreferenciaAlimentariaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PreferenciaAlimentariaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("obtener-tipo")]
        public async Task<ActionResult<List<PreferenciaAlimentariaDTO>>> GetAll()
        {
            var PreferenciaA = await context.PreferenciaAlimentaria.ToListAsync();
            if (!PreferenciaA.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<PreferenciaAlimentariaDTO>>(PreferenciaA));

        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] PreferenciaAlimentariaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PrefA = mapper.Map<PreferenciaAlimentaria>(dto);
            context.Add(PrefA);
            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message,
                    inner = ex.InnerException?.Message,
                    detalleCompleto = ex.ToString()
                });
            }
        }

        [HttpPut("actualizar/{PreferenciaAlId:int}")]
        public async Task<ActionResult> Put(int PreferenciaAlId, [FromBody] PreferenciaAlimentariaDTO dto)
        {
            if (PreferenciaAlId != dto.PreferenciaAlimentariaId)
            {
                return BadRequest("El Id de la ruta y el id del objeto no coinciden");
            }
            var PrefA = await context.PreferenciaAlimentaria.FindAsync(PreferenciaAlId);
            if (PrefA == null)
            {
                return NotFound();
            }
            mapper.Map(dto, PrefA);
            context.Update(PrefA);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreferenciaExists(PreferenciaAlId))
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
        private bool PreferenciaExists(int PreferenciaAlId)
        {
            return context.PreferenciaAlimentaria.Any(e => e.PreferenciaAlimentariaId == PreferenciaAlId);
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var PrefA = await context.PreferenciaAlimentaria.FindAsync(id);
            if (PrefA == null)
            {
                return NotFound();
            }
            context.PreferenciaAlimentaria.Remove(PrefA);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
