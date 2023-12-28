using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs.HealthStatus;

namespace VirtualPetCareAPI.Controllers;

[Route("/api/healthstatuses")]
[ApiController]
public class HealthStatusController : ControllerBase
{
    // Dependency Injection ile context kullanma
    private readonly VirtualPetCareDbContext _db;
    private readonly IMapper _mapper;

    public HealthStatusController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper)
    {
        _db = virtualPetCareDbContext;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{PetId}")] // /api/statuses/PetId
    public async Task<IActionResult> GetById(int PetId)
    {
        var healthStatus = await _db.HealthStatuses.Where(x => x.PetId == PetId).FirstOrDefaultAsync();
        if (healthStatus is null) return NotFound(); // 404
        var entity = _mapper.Map<HealthStatusDTO>(healthStatus);

        return Ok(entity); // 200
    }


    [HttpPatch]
    [Route("{PetId}")] // /api/statuses/PetId
    public async Task<IActionResult> UpdateHealthStatus(int PetId, HealthStatusDTO patchStatus)
    {
        var healthStatus = await _db.HealthStatuses.Where(x => x.PetId == PetId).FirstOrDefaultAsync();
        if (healthStatus == null) return NotFound(); // 404
        _mapper.Map(patchStatus, healthStatus);

        await _db.SaveChangesAsync();
        return NoContent(); // 204
    }
}
