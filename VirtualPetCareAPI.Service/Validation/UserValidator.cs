using FluentValidation;
using VirtualPetCareAPI.Data.DTOs;

namespace VirtualPetCareAPI.Service.Validation;

public class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name cannot be less than 3 characters")
            .MaximumLength(20).WithMessage("Name cannot exceed 20 characters");
    }
}
