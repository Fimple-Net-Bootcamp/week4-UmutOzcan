using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    // Dependency Injection ile context kullanma
    private readonly VirtualPetCareDbContext _db;
    private readonly IMapper _mapper;
    private readonly IValidator<UserDTO> _validator;
    public UserController(VirtualPetCareDbContext virtualPetCareDbContext, IMapper mapper, IValidator<UserDTO> validator)
    {
        _db = virtualPetCareDbContext;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpPost] // /api/users
    public async Task<IActionResult> Create(UserDTO newUser)
    {
        var result = _validator.Validate(newUser);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
            .ToList();

            return BadRequest(errorMessages);
        }

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
