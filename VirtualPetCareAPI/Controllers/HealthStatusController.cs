using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.DBOperations;
using VirtualPetCareAPI.Entities;
namespace VirtualPetCareAPI.Controllers;

[Route("/api/healthstatuses")]
[ApiController]
public class HealthStatusController : ControllerBase
{
    // Dependency Injection ile context kullanma
    private readonly VirtualPetCareDbContext _db;
    public HealthStatusController(VirtualPetCareDbContext virtualPetCareDbContext)
    {
        _db = virtualPetCareDbContext;
    }

    [HttpGet]
    [Route("{PetId}")] // /api/statuses/PetId
    public IActionResult GetById(int PetId)
    {
        var healthStatus =  _db.HealthStatuses.Where(x => x.PetId == PetId).FirstOrDefault(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner

        if (healthStatus is null) return NotFound(); // 404
        return Ok(healthStatus); // 200
    }


    [HttpPatch]
    [Route("{PetId}")] // /api/statuses/PetId
    public async Task<IActionResult> UpdateHealthStatus(int PetId, JsonPatchDocument<HealthStatus> patchDocument)
    {
        if (patchDocument == null) return BadRequest();

        var pet = await _db.Pets
            .Include(p => p.HealthStatuses)
            .FirstOrDefaultAsync(p => p.PetId == PetId);

        if (pet == null) return NotFound(); // 404
        var healthStatus = pet.HealthStatuses.FirstOrDefault();

        if (healthStatus == null) return NotFound(); // 404
        patchDocument.ApplyTo(healthStatus, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState); // 400

        await _db.SaveChangesAsync();
        return NoContent(); // 204
    }
}
