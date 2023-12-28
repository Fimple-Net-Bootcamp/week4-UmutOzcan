using System.ComponentModel.DataAnnotations;
namespace VirtualPetCareAPI.Data.DTOs.SocialInteraction;

public class SocialInteractionDTO
{
    public int PetId { get; set; }

    [Required(ErrorMessage = $"Description is required {nameof(Description)}")]
    [StringLength(30, ErrorMessage = $"Description must be less than 30 characters {nameof(Description)}")]
    public string Description { get; set; }
}
