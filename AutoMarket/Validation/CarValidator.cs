using FluentValidation;
using AutoMarket.Models;
 namespace AutoMarket.Validation
 {
    public class CarValidator : AbstractValidator<Car>
    {
      public CarValidator()
            {
        RuleFor(car => car.FirstRegistration).NotEmpty().WithMessage("First registration date is required.");
        RuleFor(car => car.EnginePower).NotEmpty().GreaterThan(0).WithMessage("Engine power must be greater than 0.");
        RuleFor(car => car.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(car => car.Features).NotEmpty().WithMessage("Features are required.");
        RuleFor(car => car.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(car => car.Location).NotEmpty().WithMessage("Location is required.");
        RuleFor(car => car.CarBrandId).NotEmpty().GreaterThan(0).WithMessage("Car brand is required.");
        RuleFor(car => car.CarModelId).NotEmpty().GreaterThan(0).WithMessage("Car model is required.");
        RuleFor(car => car.CarCondition).NotEmpty().WithMessage("Car condition is required.");
        RuleFor(car => car.CarColor).NotEmpty().WithMessage("Car color is required.");
        RuleFor(car => car.CarFuelType).NotEmpty().WithMessage("Fuel type is required.");
        RuleFor(car => car.CarMileage).NotEmpty().WithMessage("Mileage is required.");
        RuleFor(car => car.CarSeats).NotEmpty().WithMessage("Number of seats is required.");
        RuleFor(car => car.CarTransmissionType).NotEmpty().WithMessage("Transmission type is required.");
        RuleFor(car => car.Photos).NotEmpty().WithMessage("Photos are required.");
      }
    }
 }

