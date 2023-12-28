using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs.Nutrient;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Controllers
{
    [Route("api/nutrients")]
    [ApiController]
    public class NutrientController : ControllerBase
    {
        // Dependency Injection ile context kullanma
        private readonly VirtualPetCareDbContext _db;
        private readonly IMapper _mapper;
        public NutrientController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper)
        {
            _db = virtualPetCareDbContext;
            _mapper = mapper;
        }


        [HttpPost] // /api/nutrients
        public async Task<IActionResult> Create(NutrientDTO newNutrient)
        {
            var entity = _mapper.Map<NutrientDTO, Nutrient>(newNutrient);
            _db.Nutrients.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { entity.PetId }, entity); // olusturulan kaynagin bilgilerini donder 201
        }

        [HttpGet]
        [Route("{PetId}")] // /api/nutrients/PetId
        public async Task<IActionResult> GetById(int PetId)
        {
            var nutrients = await _db.Nutrients.Where(x => x.PetId == PetId).ToListAsync();
            if (!nutrients.Any()) return NotFound(); // 404
            var entities = _mapper.Map<List<NutrientDTO>>(nutrients);

            return Ok(entities); // 200
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nutrients = await _db.Nutrients.ToListAsync(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner
            if (!nutrients.Any()) return NotFound(); // 404
            var entities = _mapper.Map<List<NutrientDTO>>(nutrients);

            return Ok(entities); // 200
        }
    }
}
