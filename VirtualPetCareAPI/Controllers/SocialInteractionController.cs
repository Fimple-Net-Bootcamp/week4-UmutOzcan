﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs.SocialInteraction;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Controllers
{
    [Route("api/socialInteractions")]
    [ApiController]
    public class SocialInteractionController : ControllerBase
    {
        // Dependency Injection ile context kullanma
        private readonly VirtualPetCareDbContext _db;
        private readonly IMapper _mapper;

        public SocialInteractionController(VirtualPetCareDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost] // /api/socialInteractions
        public async Task<IActionResult> Create(SocialInteractionDTO newSocialInteraction)
        {
            var entity = _mapper.Map<SocialInteractionDTO, SocialInteraction>(newSocialInteraction);
            _db.SocialInteractions.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { entity.PetId }, entity); // olusturulan kaynagin bilgilerini donder 201
        }

        [HttpGet]
        [Route("{PetId}")] // /api/socialInteracitons/PetId
        public async Task<IActionResult> GetById(int PetId)
        {
            var interactions = await _db.SocialInteractions.Where(x => x.PetId == PetId).ToListAsync();
            if (!interactions.Any()) return NotFound(); // 404
            var entities = _mapper.Map<List<SocialInteractionDTO>>(interactions);

            return Ok(entities); // 200
        }
    }
}
