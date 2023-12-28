using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.DBOperations;
using VirtualPetCareAPI.Service.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Database service eklendi
builder.Services.AddDbContext<VirtualPetCareDbContext>(options =>
                                                       options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// AutoMapper islemleri
builder.Services.AddAutoMapper(typeof(MapProfile));

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
