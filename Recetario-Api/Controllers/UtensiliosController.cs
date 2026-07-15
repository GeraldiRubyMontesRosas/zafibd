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
    public class UtensiliosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UtensiliosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<UtensiliosDTO>>> GetAll()
        {
            var utencilios = await context.Utensilio.ToListAsync();
            if (!utencilios.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<UtensiliosDTO>>(utencilios));
        }
        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] UtensiliosDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var utensilios = mapper.Map<Utensilio>(dto);
            context.Add(utensilios);
            try
            {

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar los utensilios.", details = ex.Message });
            }
        }
        [HttpDelete("eliminar/{IdUtensilio:int}")]
        public async Task<ActionResult> Delete(int IdUtensilio)
        {
            var utensilio = await context.Utensilio.FindAsync(IdUtensilio);
            if (utensilio == null)
            {
                return NotFound();
            }
            context.Utensilio.Remove(utensilio);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("actualizar")]
        public async Task<ActionResult> Put(int IdUtensilio, [FromBody] UtensiliosDTO dto)

        {
            if (IdUtensilio != dto.UtensilioId)
            {
                return BadRequest("El ID de la ruta y el Id del objeto no coinciden");
            }
            var utensilio = await context.Utensilio.FindAsync(IdUtensilio);
            if (utensilio == null)
            {
                return NotFound();
            }
            mapper.Map(dto, utensilio);
            context.Update(utensilio);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtensilioExists(IdUtensilio))
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
        private bool UtensilioExists(int IdUtensilio)
        {
            return context.Utensilio.Any(e => e.UtensilioId == IdUtensilio);
        }
    }
}

