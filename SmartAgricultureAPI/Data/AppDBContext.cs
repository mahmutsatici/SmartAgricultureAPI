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
            // Özel ilişkiler veya kurallar burada tanımlanabilir.
        }
    }
}
