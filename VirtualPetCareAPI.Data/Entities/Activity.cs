using System.ComponentModel.DataAnnotations.Schema;
using VirtualPetCareAPI.Data.Entities.Abstract;

namespace VirtualPetCareAPI.Data.Entities;

public class Activity : BaseEntity<int>
{
    public string ActivityType { get; set; }

    [ForeignKey("PetId")]
    public int PetId { get; set; }
    public virtual Pet Pets { get; set; }
}
