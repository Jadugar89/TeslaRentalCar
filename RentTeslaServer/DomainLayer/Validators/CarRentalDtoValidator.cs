using DomainLayer.ModelDtos;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class CarRentalDtoValidator : AbstractValidator<CarRentalDto>
    {
        public CarRentalDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Car rental ID is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Car rental ID must be greater than or equal to 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Car rental name is required.")
                .MaximumLength(50).WithMessage("Car rental name must be at most 50 characters long.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must be at most 50 characters long.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(50).WithMessage("Street must be at most 50 characters long.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.");
            //.Matches(@"^\d{2}-\d{3}$").WithMessage("Postal code must be in the format xx-xxx.");
        }
    }
}
