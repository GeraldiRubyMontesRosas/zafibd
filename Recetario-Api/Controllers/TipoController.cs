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
    public class TipoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet("obtener-tipo")]
        public async Task<ActionResult<List<TipoDTO>>> GetAll()
        {
            var tipos = await context.Tipo.ToListAsync();
            if (!tipos.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<TipoDTO>>(tipos));

        }
        [HttpPost("crear")]
        public async Task<ActionResult> Post([FromBody] TipoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tipo = mapper.Map<Tipo>(dto);
            context.Add(tipo);
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
        [HttpPut("actualizar/{TipoId:int}")]
        public async Task<ActionResult> Put(int TipoId, [FromBody] TipoDTO dto)
        {
            if (TipoId != dto.TipoId)
            {
                return BadRequest("El Id de la ruta y el id del objeto no coinciden");
            }
            var tipo = await context.Tipo.FindAsync(TipoId);
            if (tipo == null)
            {
                return NotFound();
            }
            mapper.Map(dto, tipo);
            context.Update(tipo);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExists(TipoId))
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
        private bool TipoExists(int TipoId)
        {
            return context.Tipo.Any(e => e.TipoId == TipoId);
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipo = await context.Tipo.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            context.Tipo.Remove(tipo);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
