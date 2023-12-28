using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs.Training;
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

        public TrainingController(VirtualPetCareDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost] // /api/trainings
        public async Task<IActionResult> Create(TrainingDTO newTraining)
        {
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
