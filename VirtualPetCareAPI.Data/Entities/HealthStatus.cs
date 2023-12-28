using System.ComponentModel.DataAnnotations.Schema;
using VirtualPetCareAPI.Data.Entities.Abstract;

namespace VirtualPetCareAPI.Data.Entities;

public class HealthStatus : BaseEntity<int>
{
    public string Status { get; set; }

    [ForeignKey("PetId")]
    public int PetId { get; set; }
    public virtual Pet Pets { get; set; }
}
