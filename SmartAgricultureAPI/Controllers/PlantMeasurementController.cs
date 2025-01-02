using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAgricultureAPI.Data;
using SmartAgricultureAPI.Models;
using SmartAgricultureAPI.DataTransferObjects;
using AutoMapper;

namespace SmartAgricultureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantMeasurementController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public PlantMeasurementController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 1. GET: api/PlantMeasurement (Tüm ölçümleri getir - DTO ile)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantMeasurementDto>>> GetPlantMeasurements()
        {
            var measurements = await _context.PlantMeasurements.Include(pm => pm.Plant).ToListAsync();
            var measurementDtos = _mapper.Map<List<PlantMeasurementDto>>(measurements); // Dönüşüm
            return Ok(measurementDtos);
        }

        // 2. GET: api/PlantMeasurement/{id} (Belirli bir ölçümü getir - DTO ile)
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantMeasurementDto>> GetPlantMeasurement(int id)
        {
            var measurement = await _context.PlantMeasurements.Include(pm => pm.Plant)
                .FirstOrDefaultAsync(pm => pm.PlantMeasurementId == id);

            if (measurement == null)
            {
                return NotFound();
            }

            var measurementDto = _mapper.Map<PlantMeasurementDto>(measurement); // Dönüşüm
            return Ok(measurementDto);
        }

        // 3. POST: api/PlantMeasurement (Yeni bir ölçüm ekle - DTO ile)
        [HttpPost]
        public async Task<ActionResult<PlantMeasurementDto>> CreatePlantMeasurement(PlantMeasurementDto measurementDto)
        {
            var measurement = _mapper.Map<PlantMeasurement>(measurementDto); // Dönüşüm
            _context.PlantMeasurements.Add(measurement);
            await _context.SaveChangesAsync();

            var createdMeasurementDto = _mapper.Map<PlantMeasurementDto>(measurement); // Dönüşüm
            return CreatedAtAction(nameof(GetPlantMeasurement), new { id = measurement.PlantMeasurementId }, createdMeasurementDto);
        }

        // 4. PUT: api/PlantMeasurement/{id} (Ölçüm bilgilerini güncelle - DTO ile)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlantMeasurement(int id, PlantMeasurementDto measurementDto)
        {
            if (id != measurementDto.PlantMeasurementId)
            {
                return BadRequest();
            }

            var measurement = _mapper.Map<PlantMeasurement>(measurementDto); // Dönüşüm
            measurement.PlantMeasurementId = id; // ID'nin değişmediğinden emin olun

            _context.Entry(measurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantMeasurementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // 5. DELETE: api/PlantMeasurement/{id} (Ölçümü sil)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantMeasurement(int id)
        {
            var measurement = await _context.PlantMeasurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            _context.PlantMeasurements.Remove(measurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantMeasurementExists(int id)
        {
            return _context.PlantMeasurements.Any(e => e.PlantMeasurementId == id);
        }
    }
}
