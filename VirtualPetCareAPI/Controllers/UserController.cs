using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.DBOperations;
using VirtualPetCareAPI.Entities;
namespace VirtualPetCareAPI.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    // Dependency Injection ile context kullanma
    private readonly VirtualPetCareDbContext _db;
    public UserController(VirtualPetCareDbContext virtualPetCareDbContext)
    {
        _db = virtualPetCareDbContext;
    }

    [HttpPost] // /api/users
    public async Task<IActionResult> Create(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id= user.UserId }, user ); // olusturulan kaynagin bilgilerini donder 201
    }

    [HttpGet]
    [Route("{id}")] // /api/users/id
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _db.Users
            .Include(u => u.Pets)
            .Where(x => x.UserId == id)
            .FirstOrDefaultAsync(); // First'de garanti deger olmali, FirstOrDefault ile yoksa null doner

        if(user is null) return NotFound(); // 404
        return Ok(user); // 200
    }
}
