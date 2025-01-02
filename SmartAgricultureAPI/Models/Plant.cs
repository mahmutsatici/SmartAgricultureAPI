namespace SmartAgricultureAPI.Models
{
    public class Plant
    {
        public int PlantId { get; set; } // Primary Key
        public string PlantName { get; set; } // Bitki Adı
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Kaydedilme Tarihi

        // Navigation Property
        public ICollection<PlantMeasurement> Measurements { get; set; }
    }
}
