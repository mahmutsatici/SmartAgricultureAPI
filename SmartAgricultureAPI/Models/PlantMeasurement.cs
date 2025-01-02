namespace SmartAgricultureAPI.Models
{
    public class PlantMeasurement
    {
        public int MeasurementId { get; set; } // Primary Key
        public int PlantId { get; set; } // Foreign Key (Plant Tablosu ile ilişki)

        public double SoilMoisture { get; set; } // Toprak Nemi (%)
        public double SoilTemperature { get; set; } // Toprak Sıcaklığı (°C)
        public double SoilPH { get; set; } // Toprak PH Değeri
        public double AirTemperature { get; set; } // Hava Sıcaklığı (°C)
        public double AirHumidity { get; set; } // Hava Nem Oranı (%)
        public double LightLevel { get; set; } // Işık Seviyesi (Lux)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Ölçüm Zamanı

        // Navigation Property
        public Plant Plant { get; set; }
    }
}
