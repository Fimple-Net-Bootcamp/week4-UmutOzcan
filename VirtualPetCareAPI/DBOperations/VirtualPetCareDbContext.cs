using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Entities;

namespace VirtualPetCareAPI.DBOperations;

public class VirtualPetCareDbContext : DbContext // Db tanımlamaları
{
    // Programdaki AddDbContext e yonlendiriyoruz
    public VirtualPetCareDbContext(DbContextOptions<VirtualPetCareDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<HealthStatus> HealthStatuses { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Nutrient> Nutrients { get; set;}
}
