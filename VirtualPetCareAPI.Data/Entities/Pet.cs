using System.ComponentModel.DataAnnotations.Schema;
using VirtualPetCareAPI.Data.Entities.Abstract;

namespace VirtualPetCareAPI.Data.Entities;

public class Pet : BaseEntity<int>
{
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }
    public DateTime? DateOfBirth { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual HealthStatus HealthStatus { get; set; }
    public virtual List<Activity> Activities { get; set; }
    public virtual List<Nutrient> Nutrients { get; set; }
    public virtual List<Training> Trainings { get; set; }
    public virtual List<SocialInteraction> SocialInteractions { get; set; }
}