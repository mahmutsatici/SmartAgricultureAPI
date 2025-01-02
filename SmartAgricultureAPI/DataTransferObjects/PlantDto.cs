using SmartAgricultureAPI.Models;

namespace SmartAgricultureAPI.DataTransferObjects
{
    public class PlantDto
    {
        public int PlantId { get; set; } // Primary Key
        public string PlantName { get; set; } // Bitki Adı
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Kaydedilme Tarihi

        public ICollection<PlantMeasurementDto>? Measurements { get; set; }
    }
}
