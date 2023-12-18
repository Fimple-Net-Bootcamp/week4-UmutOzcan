using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualPetCareAPI.DBOperations;
using VirtualPetCareAPI.Entities;

namespace VirtualPetCareAPI.Controllers
{
    [Route("api/nutrients")]
    [ApiController]
    public class NutrientController : ControllerBase
    {
        // Dependency Injection ile context kullanma
        private readonly VirtualPetCareDbContext _db;
        public NutrientController(VirtualPetCareDbContext virtualPetCareDbContext)
        {
            _db = virtualPetCareDbContext;
        }


        [HttpPost] // /api/nutrients
        public IActionResult Create(Nutrient nutrient)
        {
            _db.Nutrients.Add(nutrient);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = nutrient.PetId }, nutrient); // olusturulan kaynagin bilgilerini donder 201
        }

        [HttpGet]
        [Route("{PetId}")] // /api/nutrients/PetId
        public IActionResult GetById(int PetId)
        {
            var nutrient = _db.Nutrients.Where(x => x.PetId == PetId).FirstOrDefault(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner

            if (nutrient is null) return NotFound(); // 404
            return Ok(nutrient); // 200
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var nutrients = _db.Nutrients.ToList(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner

            if (!nutrients.Any()) return NotFound(); // 404
            return Ok(nutrients); // 200
        }
    }
}
