using Microsoft.EntityFrameworkCore;
using SmartAgricultureAPI.Models;

namespace SmartAgricultureAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        // DbSet'ler
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantMeasurement> PlantMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Plant - PlantMeasurement ilişkisi
            modelBuilder.Entity<Plant>()
                .HasMany(p => p.Measurements)
                .WithOne(m => m.Plant)
                .HasForeignKey(m => m.PlantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
