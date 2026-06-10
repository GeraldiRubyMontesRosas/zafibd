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
    public class PasoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PasoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<PasosDTO>>> GetAll()
        {
            var pasos = await context.Paso.ToListAsync();
            if (!pasos.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<PasosDTO>>(pasos));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] PasosDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paso = mapper.Map<Paso>(dto);
            context.Add(paso);
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
            var paso = await context.Paso.FindAsync(id);
            if (paso == null)
            {
                return NotFound();
            }
            context.Paso.Remove(paso);
            await context.SaveChangesAsync();
            return NoContent();
        }

       

        [HttpPut("actualizar/{Pasoid:int}")]
        public async Task<ActionResult> Put(int Pasoid, [FromBody] PasosDTO dto)
        {
            if (Pasoid != dto.PasoId)
            {
                return BadRequest("El ID de la ruta y el Id del objeto no coinciden");
            }
            var paso = await context.Paso.FindAsync(Pasoid);
            if (paso == null)
            {
                return NotFound();
            }
            mapper.Map(dto, paso);
            context.Update(paso);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoExists(Pasoid))
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
        private bool PasoExists(int PasoId)
        {
            return context.Paso.Any(e => e.PasoId == PasoId);
        }
    }
}
