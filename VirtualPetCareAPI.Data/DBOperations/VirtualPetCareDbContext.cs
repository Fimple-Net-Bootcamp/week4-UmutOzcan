using Microsoft.EntityFrameworkCore;
using VirtualPetCareAPI.Data.Entities;

namespace VirtualPetCareAPI.Data.DBOperations;

public class VirtualPetCareDbContext : DbContext // Db tanımlamaları
{
    // Programdaki AddDbContext e yonlendiriyoruz
    public VirtualPetCareDbContext(DbContextOptions<VirtualPetCareDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<HealthStatus> HealthStatuses { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Nutrient> Nutrients { get; set; }
    public DbSet<SocialInteraction> SocialInteractions { get; set; }
    public DbSet<Training> Trainings { get; set; }
}
