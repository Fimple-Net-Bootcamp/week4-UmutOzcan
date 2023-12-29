using FluentValidation;
using VirtualPetCareAPI.Data.DTOs;

namespace VirtualPetCareAPI.Service.Validation;

public class HealthStatusValidator : AbstractValidator<HealthStatusDTO>
{
    public HealthStatusValidator()
    {
        RuleFor(healthStatus => healthStatus.Status)
            .NotEmpty().WithMessage("Status is required")
            .MinimumLength(3).WithMessage("Status cannot be less than 3 characters")
            .MaximumLength(30).WithMessage("Status cannot exceed 30 characters");

        RuleFor(healthStatus => healthStatus.PetId)
            .NotEmpty().WithMessage("PetId is required");
    }
}
