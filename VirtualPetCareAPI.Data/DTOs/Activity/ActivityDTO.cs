using System.ComponentModel.DataAnnotations;
namespace VirtualPetCareAPI.Data.DTOs.Activity;

public class ActivityDTO
{
    public int PetId { get; set; }

    [Required(ErrorMessage = $"ActivityType is required {nameof(ActivityType)}")]
    [StringLength(30, ErrorMessage = $"ActivityType must be less than 30 characters {nameof(ActivityType)}")]
    public string ActivityType { get; set; }
}
