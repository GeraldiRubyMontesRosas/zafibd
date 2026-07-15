using AutoMapper;
using Recetario_Api.DTOs;
using Recetario_Api.Entities;

namespace Recetario_Api.Utilities
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            // source, destination
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();

            CreateMap<IngredienteDTO, Ingrediente>();
            CreateMap<Ingrediente, IngredienteDTO>();

            CreateMap<IngredienteRecetaDTO, IngredienteReceta>();
            CreateMap<IngredienteReceta, IngredienteRecetaDTO>();

            CreateMap<PasosDTO, Paso>();
            CreateMap<Paso, PasosDTO>();

            CreateMap<RecetasDTO, Receta>();
            CreateMap<Receta, RecetasDTO>();

            CreateMap<UnidadDTO, Unidad>();
            CreateMap<Unidad, UnidadDTO>();

            CreateMap<UtensiliosDTO, Utensilio>();
            CreateMap<Utensilio, UtensiliosDTO>();

            CreateMap<TipoDTO, Tipo>();
            CreateMap<Tipo, TipoDTO>();

            CreateMap<PreferenciaAlimentariaDTO, PreferenciaAlimentaria>();
            CreateMap<PreferenciaAlimentaria, PreferenciaAlimentariaDTO>();
        }
    }
}
