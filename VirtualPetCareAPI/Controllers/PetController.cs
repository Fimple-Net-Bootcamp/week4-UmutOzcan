using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs.Pet;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Controllers;

[ApiController]
[Route("/api/pets")]
public class PetController : ControllerBase
{
    // Dependency Injection ile context kullanma
    private readonly VirtualPetCareDbContext _db;
    private readonly IMapper _mapper;
    public PetController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper)
    {
        _db = virtualPetCareDbContext;
        _mapper = mapper;
    }

    [HttpPost] // /api/pets
    public async Task<IActionResult> Create(PetDTO newPet)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        var entity = _mapper.Map<Pet>(newPet);
        _db.Pets.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { entity.Id }, newPet); // olusturulan kaynagin bilgilerini donder 201
    }

    // lazy loading açık includea gerek yok
    [HttpGet] // /api/pets
    public async Task<IActionResult>  GetAll()
    {
        var pets = await _db.Pets.ToListAsync();
        if (!pets.Any()) return NotFound(); // 404
        var entities = _mapper.Map<List<PetDTO>>(pets);

        return Ok(entities); // 200
    }

    [HttpGet]
    [Route("{PetId}")] // /api/pets/id
    public async Task<IActionResult> GetById(int PetId)
    {
        var pet = await _db.Pets.Where(x => x.Id == PetId).FirstOrDefaultAsync(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner
        if (pet is null) return NotFound(); // 404
        var entity = _mapper.Map<PetDTO>(pet);

        return Ok(entity); // 200
    }

    [HttpPut("{id}")] // /api/pets/id
    public async Task<IActionResult> Update(int id, PetDTO pet)
    {
        var updatedPet = await _db.Pets.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (updatedPet is null) return NotFound(); // 404
        _mapper.Map(pet,updatedPet);
        await _db.SaveChangesAsync();

        return NoContent(); // 200
    }

}