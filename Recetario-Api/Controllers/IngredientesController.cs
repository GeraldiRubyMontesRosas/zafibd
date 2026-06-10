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
    public class IngredientesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IngredientesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

       
        [HttpGet("obtener-ingredientes")]
        public async Task<ActionResult<List<IngredienteDTO>>> GetAll()
        {
            var ingredientes = await context.Ingrediente.ToListAsync();
            if (!ingredientes.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<IngredienteDTO>>(ingredientes));

        }

   
        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] IngredienteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ingrediente = mapper.Map<Ingrediente>(dto);
            context.Add(ingrediente);
            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpPut("actualizar/{IngredienteId:int}")]
        public async Task<ActionResult> Put(int IngredienteId, [FromBody] IngredienteDTO dto)
        {
            if(IngredienteId != dto.IngredienteId)
            {
                return BadRequest("El Id de la ruta y el id del objeto no coinciden");
            }
            var ingrediente = await context.Ingrediente.FindAsync(IngredienteId);
            if (ingrediente == null)
            {
                return NotFound();
            }
            mapper.Map(dto, ingrediente);
            context.Update(ingrediente);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(IngredienteId))
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
        private bool IngredienteExists(int IngredienteId) 
        {
            return context.Ingrediente.Any(e => e.IngredienteId == IngredienteId);
        }

        [HttpDelete("eliminar/{IngredienteId}")]
        public async Task<ActionResult> Delete(int IngredienteId)
        {
            var ingrediente = await context.Ingrediente.FindAsync(IngredienteId);
            if( ingrediente == null)
            {
                return NotFound();
            }
            context.Ingrediente.Remove(ingrediente);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
