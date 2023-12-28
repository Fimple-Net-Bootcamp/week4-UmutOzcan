using System.ComponentModel.DataAnnotations;
namespace VirtualPetCareAPI.Data.DTOs.Pet;

public class PetDTO
{
    public int UserId { get; set; }

    [Required(ErrorMessage = $"Name is required {nameof(Name)}")]
    [StringLength(20, ErrorMessage = $"Name must be less than 20 characters {nameof(Name)}")]
    public string Name { get; set; }

    [Required(ErrorMessage = $"Species is required {nameof(Species)}")]
    [StringLength(10, ErrorMessage = $"Species must be less than 10 characters {nameof(Species)}")]
    public string Species { get; set; }

    [Required(ErrorMessage = $"Age is required {nameof(Age)}")]
    [Range(0, 100, ErrorMessage = $"Age must be between 0 and 100 {nameof(Age)}")]
    public int Age { get; set; }
}
