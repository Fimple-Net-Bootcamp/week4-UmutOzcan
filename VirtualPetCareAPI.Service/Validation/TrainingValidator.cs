using FluentValidation;
using VirtualPetCareAPI.Data.DTOs;

namespace VirtualPetCareAPI.Service.Validation;

public class TrainingValidator : AbstractValidator<TrainingDTO>
{
    public TrainingValidator()
    {
        RuleFor(training => training.PetId)
            .NotEmpty().WithMessage("PetId is required");

        RuleFor(training => training.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(1).WithMessage("Description cannot be less than 1 characters")
            .MaximumLength(30).WithMessage("Description cannot exceed 30 characters");
    }
}
