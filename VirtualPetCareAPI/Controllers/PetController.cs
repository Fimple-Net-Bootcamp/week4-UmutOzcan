using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.DBOperations;
using VirtualPetCareAPI.Entities;

namespace VirtualPetCareAPI.Controllers;

[ApiController]
[Route("/api/pets")]
public class PetController : ControllerBase
{
    // Dependency Injection ile context kullanma
    private readonly VirtualPetCareDbContext _db;
    public PetController(VirtualPetCareDbContext virtualPetCareDbContext)
    {
        _db = virtualPetCareDbContext;
    }

    [HttpPost] // /api/pets
    public async Task<IActionResult> Create(Pet pet)
    {
        _db.Pets.Add(pet);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = pet.PetId }, pet); // olusturulan kaynagin bilgilerini donder 201
    }

    [HttpGet] // /api/pets
    public async Task<IActionResult>  GetAll()
    {
        var pets = await _db.Pets
            .Include(p => p.Nutrients)
            .Include(p => p.Activities)
            .Include(p => p.HealthStatuses)
            .ToListAsync();

        if (!pets.Any()) return NotFound(); // 404
        return Ok(pets); // 200
    }

    [HttpGet]
    [Route("{id}")] // /api/pets/id
    public async Task<IActionResult> GetById(int id)
    {
        var pet = await _db.Pets
            .Include(p => p.Nutrients)
            .Include(p => p.Activities)
            .Include(p => p.HealthStatuses)
            .Where(x => x.PetId == id)
            .FirstOrDefaultAsync(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner

        if (pet is null) return NotFound(); // 404
        return Ok(pet); // 200
    }

    [HttpPut("{id}")] // /api/pets/id
    public async Task<IActionResult> Update(int id, Pet pet)
    {
        var current = _db.Pets.Where(x => x.PetId == id).FirstOrDefault();

        if(current is null) return NotFound(); // 404
        if (!ModelState.IsValid) return BadRequest(ModelState); // 400

        current.PetName = pet.PetName;
        current.Species = pet.Species;
        current.DateOfBirth = pet.DateOfBirth;
        await _db.SaveChangesAsync();

        return Ok(current); // 200
    }

}