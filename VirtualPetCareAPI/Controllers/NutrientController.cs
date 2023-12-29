using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs;
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
        private readonly IValidator<NutrientDTO> _validator;
        public NutrientController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper, IValidator<NutrientDTO> validator)
        {
            _db = virtualPetCareDbContext;
            _mapper = mapper;
            _validator = validator;
        }


        [HttpPost] // /api/nutrients
        public async Task<IActionResult> Create(NutrientDTO newNutrient)
        {
            var result = _validator.Validate(newNutrient);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                .ToList();

                return BadRequest(errorMessages);
            }

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
