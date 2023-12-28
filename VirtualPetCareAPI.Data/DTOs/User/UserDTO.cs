using System.ComponentModel.DataAnnotations;
namespace VirtualPetCareAPI.Data.DTOs.User;

public class UserDTO
{
    [Required(ErrorMessage = $"Name is required {nameof(Name)}")]
    [StringLength(30, ErrorMessage = $"Name must be less than 30 characters {nameof(Name)}")]
    public string Name { get; set; }
}