using System.ComponentModel.DataAnnotations.Schema;
using VirtualPetCareAPI.Data.Entities.Abstract;

namespace VirtualPetCareAPI.Data.Entities;

public class Nutrient : BaseEntity<int>
{
    public string NutrientType { get; set; }
    public string EatingFrequency { get; set; }

    [ForeignKey("PetId")]
    public int PetId { get; set; }
    public virtual Pet Pets { get; set; }
}
