using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Data.DTOs;
using VirtualPetCareAPI.Service.Mapping;
using VirtualPetCareAPI.Service.Validation;

var builder = WebApplication.CreateBuilder(args);

// Database Context MSSQL configuration
builder.Services.AddDbContext<VirtualPetCareDbContext>(options =>
                                                       options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));

// Fluent Validation
builder.Services.AddSingleton<IValidator<ActivityDTO>, ActivityValidator>();
builder.Services.AddSingleton<IValidator<HealthStatusDTO>, HealthStatusValidator>();
builder.Services.AddSingleton<IValidator<NutrientDTO>, NutrientValidator>();
builder.Services.AddSingleton<IValidator<UserDTO>, UserValidator>();
builder.Services.AddSingleton<IValidator<PetDTO>, PetValidator>();
builder.Services.AddSingleton<IValidator<TrainingDTO>, TrainingValidator>();
builder.Services.AddSingleton<IValidator<SocialInteractionDTO>, SocialInteractionValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
