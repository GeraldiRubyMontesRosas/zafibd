using Microsoft.EntityFrameworkCore;
using Recetario_Api.Entities;

namespace Recetario_Api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<IngredienteReceta> IngredienteReceta { get; set; }
        public DbSet<Paso> Paso { get; set; }
        public DbSet<Receta> Receta { get; set; }
        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<Utensilio> Utensilio { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<PreferenciaAlimentaria> PreferenciaAlimentaria { get; set; }
    }
}

