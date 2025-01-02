using AutoMapper;
using SmartAgricultureAPI.Models;
using SmartAgricultureAPI.DataTransferObjects;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping işlemlerini buraya ekliyoruz
        CreateMap<Plant, PlantDto>(); // Plant'tan PlantDto'ya dönüşüm
        CreateMap<PlantMeasurement, PlantMeasurementDto>(); // PlantMeasurement'tan PlantMeasurementDto'ya dönüşüm
        // İhtiyacınız olan diğer dönüşümleri de buraya ekleyebilirsiniz
    }
}
