using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualPetCareAPI.Entities;

public class Pet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PetId { get; set; }

    [Required]
    public string PetName { get; set; }

    [Required]
    public string Species { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public required int UserId { get; set; }

    public virtual List<HealthStatus> HealthStatuses { get; set; } = new List<HealthStatus>();
    public virtual List<Activity> Activities { get; set; } = new List<Activity>();
    public virtual List<Nutrient> Nutrients { get; set; } = new List<Nutrient>();
}