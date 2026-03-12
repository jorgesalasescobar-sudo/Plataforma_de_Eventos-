using EventService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventService.Infrastructure.Persistence
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Zone> Zones { get; set; }

        // Aquí es donde configuras las entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Event
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
            });

            // Configuración de Zone
            modelBuilder.Entity<Zone>(entity =>
            {
                entity.HasKey(z => z.Id);
                entity.Property(z => z.Name).IsRequired().HasMaxLength(100);

                // Aquí defines el tipo decimal con precisión
                entity.Property(z => z.Price).HasColumnType("decimal(18,2)");

                entity.Property(z => z.Capacity).IsRequired();
            });
        }
    }
}
