using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs;
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
        private readonly IValidator<ActivityDTO> _validator;

        public ActivityController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper, IValidator<ActivityDTO> validator)
        {
            _db = virtualPetCareDbContext;
            _mapper = mapper;
            _validator = validator;
        }


        [HttpPost] // /api/activities
        public async Task<IActionResult> Create(ActivityDTO newActivity)
        {
            var result = _validator.Validate(newActivity);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                .ToList();

                return BadRequest(errorMessages);
            }

            var entity = _mapper.Map<ActivityDTO, Activity>(newActivity);
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
            var entity = _mapper.Map<List<Activity>, List<ActivityDTO>>(activities);

            return Ok(entity); // 200
        }
    }
}