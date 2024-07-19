using FluentValidation;
using AutoMarket.Models;

namespace AutoMarket.Validation
{
    public class MotorcycleValidator : AbstractValidator<Motorcycle>
    {
        public MotorcycleValidator()
        {
            RuleFor(motorcycle => motorcycle.FirstRegistration)
                .NotEmpty().WithMessage("First registration date is required.");
            RuleFor(motorcycle => motorcycle.EnginePower)
                .NotEmpty().GreaterThan(0).WithMessage("Engine power must be greater than 0.");
            RuleFor(motorcycle => motorcycle.Price)
                .NotEmpty().GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(motorcycle => motorcycle.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(motorcycle => motorcycle.Location)
                .NotEmpty().WithMessage("Location is required.");
            RuleFor(motorcycle => motorcycle.MotorcycleType)
                .IsInEnum().WithMessage("Motorcycle type is required.");
            RuleFor(motorcycle => motorcycle.Condition)
                .IsInEnum().WithMessage("Condition is required.");
            RuleFor(motorcycle => motorcycle.Color)
                .IsInEnum().WithMessage("Color is required.");
            RuleFor(motorcycle => motorcycle.FuelType)
                .IsInEnum().WithMessage("Fuel type is required.");
            RuleFor(motorcycle => motorcycle.Mileage)
                .NotEmpty().WithMessage("Mileage is required.");
            RuleFor(motorcycle => motorcycle.TransmissionType)
                .IsInEnum().WithMessage("Transmission type is required.");
            RuleFor(motorcycle => motorcycle.MotorcycleBrandId)
                .NotEmpty().GreaterThan(0).WithMessage("Motorcycle brand is required.");
            RuleFor(motorcycle => motorcycle.MotorcycleModelId)
                .NotEmpty().GreaterThan(0).WithMessage("Motorcycle model is required.");
            RuleFor(motorcycle => motorcycle.Photos)
                .NotEmpty().WithMessage("Photos are required.");
        }
    }
}
