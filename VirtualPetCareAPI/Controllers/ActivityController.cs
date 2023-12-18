using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.DBOperations;
using VirtualPetCareAPI.Entities;

namespace VirtualPetCareAPI.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        // Dependency Injection ile context kullanma
        private readonly VirtualPetCareDbContext _db;
        public ActivityController(VirtualPetCareDbContext virtualPetCareDbContext)
        {
            _db = virtualPetCareDbContext;
        }


        [HttpPost] // /api/activities
        public IActionResult Create(Activity activity)
        {
            _db.Activities.Add(activity);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = activity.PetId }, activity); // olusturulan kaynagin bilgilerini donder 201
        }

        [HttpGet]
        [Route("{PetId}")] // /api/activities/PetId
        public IActionResult GetById(int PetId)
        {
            var activity = _db.Activities.Where(x => x.PetId == PetId).FirstOrDefault(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner

            if (activity is null) return NotFound(); // 404
            return Ok(activity); // 200
        }
    }
}
