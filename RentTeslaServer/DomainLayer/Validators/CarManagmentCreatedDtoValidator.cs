using DomainLayer.ModelDtos;
using FluentValidation;


namespace DomainLayer.Validators
{
    public class CarManagmentCreatedDtoValidator : AbstractValidator<CarManagmentCreatedDto>
    {
        public CarManagmentCreatedDtoValidator()
        {
            RuleFor(x => x.IsPrepared).NotNull().WithMessage("IsPrepared is required.");

            RuleFor(x => x.IsFree).NotNull().WithMessage("IsFree is required.");

            RuleFor(x => x.DailyPrice).NotNull().WithMessage("DailyPrice is required.")
                .GreaterThanOrEqualTo(0).WithMessage("DailyPrice must be greater than or equal to 0.");

            RuleFor(x => x.Plates).NotEmpty().WithMessage("Plates is required.");

            RuleFor(x => x.CarTypeDto).NotNull().WithMessage("CarTypeDto is required.")
                .SetValidator(new CarTypeDtoValidator());

            RuleFor(x => x.CarRentalDto).NotNull().WithMessage("CarRentalDto is required.")
                 .SetValidator(new CarRentalDtoValidator());
        }
    }
}
