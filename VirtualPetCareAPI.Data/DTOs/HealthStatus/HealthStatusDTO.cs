using System.ComponentModel.DataAnnotations;
namespace VirtualPetCareAPI.Data.DTOs.HealthStatus;

public class HealthStatusDTO
{
    public int PetId { get; set; }

    [Required(ErrorMessage = $"Status is required {nameof(Status)}")]
    [StringLength(30, ErrorMessage = $"Status must be less than 30 characters {nameof(Status)}")]
    public string Status { get; set; }
}
