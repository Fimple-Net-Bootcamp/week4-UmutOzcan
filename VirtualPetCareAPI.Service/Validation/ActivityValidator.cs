using FluentValidation;
using VirtualPetCareAPI.Data.DTOs;

namespace VirtualPetCareAPI.Service.Validation;

public class ActivityValidator : AbstractValidator<ActivityDTO>
{
    public ActivityValidator()
    {
        RuleFor(activity => activity.ActivityType)
                .NotEmpty().WithMessage("ActivityType is required")
                .MinimumLength(3).WithMessage("ActivityType cannot be less than 3 characters")
                .MaximumLength(30).WithMessage("ActivityType cannot exceed 30 characters");

        RuleFor(activity => activity.PetId)
            .NotEmpty().WithMessage("PetId is required");
    }
}
