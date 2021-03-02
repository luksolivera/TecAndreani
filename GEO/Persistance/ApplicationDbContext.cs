using Domain.Entitiies;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Geolocalizacion> Geolocalizacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>(entity => 
            {
                entity.ToTable("Estado");

                entity.HasKey(x => x.EstadoId);
                entity.Property(x => x.EstadoId).ValueGeneratedOnAdd();

                entity.Property(x => x.Descripcion)
                    .HasMaxLength(50);

                entity.HasData(
                    new Estado
                    {
                        EstadoId = 1,
                        Descripcion = "Procesando"
                    },
                    new Estado
                    {
                        EstadoId = 2,
                        Descripcion = "Terminado"
                    });
            });
            modelBuilder.Entity<Geolocalizacion>(entity =>
            {
                entity.ToTable("Geolocalizacion");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.HasOne(x => x.Estado)
                    .WithMany(x => x.Geolocalizacion)
                    .HasForeignKey(x => x.EstadoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Geolocalizacion_Estado");
            });
        }
    }
}
