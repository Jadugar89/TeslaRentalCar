using FluentValidation;
using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Domain_Layer.Validators
{
    public class CarTypeDtoValidator : AbstractValidator<CarTypeDto>
    {
        public CarTypeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(x => x.Motor)
                .NotEmpty().WithMessage("Motor is required.")
                .MaximumLength(50).WithMessage("Motor cannot be longer than 50 characters.");

            RuleFor(x => x.Range)
                .GreaterThanOrEqualTo(0).WithMessage("Range must be greater than or equal to 0.");

            RuleFor(x => x.Seats)
                .GreaterThanOrEqualTo(0).WithMessage("Seats must be greater than or equal to 0.");
        }
    }
}
