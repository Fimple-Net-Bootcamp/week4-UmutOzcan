using FluentValidation;
using VirtualPetCareAPI.Data.DTOs;

namespace VirtualPetCareAPI.Service.Validation;

public class PetValidator : AbstractValidator<PetDTO>
{
    public PetValidator()
    {
        RuleFor(pet => pet.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(pet => pet.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(2).WithMessage("Name cannot be less than 2 characters")
            .MaximumLength(20).WithMessage("Name cannot exceed 20 characters");

        RuleFor(pet => pet.Species)
            .NotEmpty().WithMessage("Species is required")
            .MinimumLength(2).WithMessage("Species cannot be less than 2 characters")
            .MaximumLength(10).WithMessage("Species cannot exceed 10 characters");

        RuleFor(pet => pet.Age)
            .NotEmpty().WithMessage("Age is required")
            .InclusiveBetween(0, 100).WithMessage("Age must be between 0 and 100");
    }
}