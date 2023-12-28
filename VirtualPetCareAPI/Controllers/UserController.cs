using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs.User;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    // Dependency Injection ile context kullanma
    private readonly VirtualPetCareDbContext _db;
    private readonly IMapper _mapper;
    public UserController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper)
    {
        _db = virtualPetCareDbContext;
        _mapper = mapper;
    }

    [HttpPost] // /api/users
    public async Task<IActionResult> Create(UserDTO newUser)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        var entity = _mapper.Map<User>(newUser);
        _db.Users.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id= entity.Id }, entity ); // olusturulan kaynagin bilgilerini donder 201
    }

    // lazy loading açýk include gerek yok
    [HttpGet]
    [Route("{id}")] // /api/users/id
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (user is null) return NotFound(); // 404
        var entity = _mapper.Map<UserDTO>(user);
        return Ok(entity); // 200
    }
}
