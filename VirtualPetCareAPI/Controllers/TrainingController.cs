using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Controllers
{
    [Route("api/trainings")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        // Dependency Injection ile context kullanma
        private readonly VirtualPetCareDbContext _db;
        private readonly IMapper _mapper;
        private readonly IValidator<TrainingDTO> _validator;

        public TrainingController(VirtualPetCareDbContext db, IMapper mapper, IValidator<TrainingDTO> validator)
        {
            _db = db;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost] // /api/trainings
        public async Task<IActionResult> Create(TrainingDTO newTraining)
        {
            var result = _validator.Validate(newTraining);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                .ToList();

                return BadRequest(errorMessages);
            }

            var entity = _mapper.Map<TrainingDTO, Training>(newTraining);
            _db.Trainings.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { entity.PetId }, entity); // olusturulan kaynagin bilgilerini donder 201
        }

        [HttpGet]
        [Route("{PetId}")] // /api/trainings/PetId
        public async Task<IActionResult> GetById(int PetId)
        {
            var trainings = await _db.Trainings.Where(x => x.PetId == PetId).ToListAsync();
            if (!trainings.Any()) return NotFound(); // 404
            var entities = _mapper.Map<List<TrainingDTO>>(trainings);

            return Ok(entities); // 200
        }
    }
}
