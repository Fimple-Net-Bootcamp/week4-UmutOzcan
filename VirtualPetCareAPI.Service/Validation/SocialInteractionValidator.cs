using FluentValidation;
using VirtualPetCareAPI.Data.DTOs;

namespace VirtualPetCareAPI.Service.Validation;

public class SocialInteractionValidator : AbstractValidator<SocialInteractionDTO>
{
    public SocialInteractionValidator()
    {
        RuleFor(interaciton => interaciton.PetId)
            .NotEmpty().WithMessage("PetId is required");

        RuleFor(interaction => interaction.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(1).WithMessage("Description cannot be less than 1 characters")
            .MaximumLength(30).WithMessage("Description cannot exceed 30 characters");
    }
}