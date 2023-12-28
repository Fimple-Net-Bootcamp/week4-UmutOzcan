using System.ComponentModel.DataAnnotations;
namespace VirtualPetCareAPI.Data.DTOs.Nutrient;

public class NutrientDTO
{
    public int PetId { get; set; }

    [Required(ErrorMessage = $"NutrientType is required {nameof(NutrientType)}")]
    [StringLength(20, ErrorMessage = $"NutrientType must be less than 20 characters {nameof(NutrientType)}")]
    public string NutrientType { get; set; }

    [Required(ErrorMessage = $"EatingFrequency is required {nameof(EatingFrequency)}")]
    [StringLength(15, ErrorMessage = $"EatingFrequency must be less than 15 characters {nameof(EatingFrequency)}")]
    public string EatingFrequency { get; set; }
}
