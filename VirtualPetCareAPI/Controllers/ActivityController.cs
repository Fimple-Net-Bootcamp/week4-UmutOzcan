using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs.Activity;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        // Dependency Injection ile context kullanma
        private readonly VirtualPetCareDbContext _db;
        private readonly IMapper _mapper;

        public ActivityController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper)
        {
            _db = virtualPetCareDbContext;
            _mapper = mapper;
        }


        [HttpPost] // /api/activities
        public async Task<IActionResult> Create(ActivityDTO newActivity)
        {
            var entity = _mapper.Map<Activity>(newActivity);
            _db.Activities.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { entity.PetId }, entity); // olusturulan kaynagin bilgilerini donder 201
        }

        [HttpGet]
        [Route("{PetId}")] // /api/activities/PetId
        public async Task<IActionResult> GetById(int PetId)
        {
            var activities = await _db.Activities.Where(x => x.PetId == PetId).ToListAsync();
            if (!activities.Any()) return NotFound(); // 404
            var entity = _mapper.Map<List<ActivityDTO>>(activities);

            return Ok(entity); // 200
        }
    }
}
