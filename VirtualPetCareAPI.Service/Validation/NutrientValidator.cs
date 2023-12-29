using FluentValidation;
using VirtualPetCareAPI.Data.DTOs;

namespace VirtualPetCareAPI.Service.Validation;

public class NutrientValidator : AbstractValidator<NutrientDTO>
{
    public NutrientValidator()
    {
        RuleFor(nutrient =>  nutrient.NutrientType)
            .NotEmpty().WithMessage("NutrientType is required")
            .MinimumLength(3).WithMessage("NutrientType cannot be less than 3 characters")
            .MaximumLength(20).WithMessage("NutrientType cannot exceed 20 characters");

        RuleFor(nutrient => nutrient.EatingFrequency)
            .NotEmpty().WithMessage("EatingFrequency is required")
            .MinimumLength(3).WithMessage("EatingFrequency cannot be less than 3 characters")
            .MaximumLength(15).WithMessage("EatingFrequency cannot exceed 15 characters");

        RuleFor(nutrient => nutrient.PetId)
            .NotEmpty().WithMessage("PetId is required");

    }
}
