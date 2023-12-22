using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class HBazanLaudexContext : DbContext
    {
        public HBazanLaudexContext(DbContextOptions<HBazanLaudexContext> options) : base(options)
        {

        }

        // Tables
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<UsuarioTarea> UsuarioTareas { get; set; }
    }
}