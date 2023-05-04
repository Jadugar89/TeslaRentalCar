using RentTeslaServer.DomainLayer.ModelDtos;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class CarManagmentDetailDtoValidator : AbstractValidator<CarManagmentDetailDto>
    {
        public CarManagmentDetailDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.IsPrepared).NotNull().WithMessage("IsPrepared is required.");

            RuleFor(x => x.IsFree).NotNull().WithMessage("IsFree is required.");

            RuleFor(x => x.DailyPrice).NotNull().WithMessage("DailyPrice is required.")
                .GreaterThan(0).WithMessage("DailyPrice must be greater than or equal to 0.");

            RuleFor(x => x.Plates).NotEmpty().WithMessage("Plates are required.");

            RuleFor(x => x.CarTypeDto).NotNull().WithMessage("CarTypeDto is required.")
                .SetValidator(new CarTypeDtoValidator());

            RuleFor(x => x.CarRentalDto).NotNull().WithMessage("CarRentalDto is required.")
                 .SetValidator(new CarRentalDtoValidator());
        }

    }
}
