using AutoMapper; // AutoMapper kullanarak dönüşümleri kolaylaştırabilirsiniz
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAgricultureAPI.Data;
using SmartAgricultureAPI.Models;
using SmartAgricultureAPI.DataTransferObjects;

namespace SmartAgricultureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public PlantController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 1. GET: api/Plant (Tüm bitkileri getir - DTO ile)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDto>>> GetPlants()
        {
            var plants = await _context.Plants.Include(p => p.Measurements).ToListAsync();
            var plantDtos = _mapper.Map<List<PlantDto>>(plants); // Plant -> PlantDto dönüşümü
            return Ok(plantDtos);
        }

        // 2. GET: api/Plant/{id} (Belirli bir bitkiyi getir - DTO ile)
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDto>> GetPlant(int id)
        {
            var plant = await _context.Plants.Include(p => p.Measurements).FirstOrDefaultAsync(p => p.PlantId == id);

            if (plant == null)
            {
                return NotFound();
            }

            var plantDto = _mapper.Map<PlantDto>(plant); // Plant -> PlantDto dönüşümü
            return Ok(plantDto);
        }

        // 3. POST: api/Plant (Yeni bir bitki ekle - DTO ile)
        [HttpPost]
        public async Task<ActionResult<PlantDto>> CreatePlant(PlantDto plantDto)
        {
            var plant = _mapper.Map<Plant>(plantDto); // PlantDto -> Plant dönüşümü
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            var createdPlantDto = _mapper.Map<PlantDto>(plant); // Yeni eklenen Plant -> PlantDto dönüşümü
            return CreatedAtAction(nameof(GetPlant), new { id = plant.PlantId }, createdPlantDto);
        }

        // 4. PUT: api/Plant/{id} (Bitki bilgilerini güncelle - DTO ile)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, PlantDto plantDto)
        {
            if (id != plantDto.PlantId)
            {
                return BadRequest();
            }

            var plant = _mapper.Map<Plant>(plantDto); // PlantDto -> Plant dönüşümü
            plant.PlantId = id; // ID'nin değişmediğinden emin olun

            _context.Entry(plant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
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

        // 5. DELETE: api/Plant/{id} (Bitkiyi sil)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.PlantId == id);
        }
    }
}
