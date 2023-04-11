using FluentValidation;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Domain_Layer.Validators
{

    public class ReservationValidator : AbstractValidator<ReservationCreateDto>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.Reservation.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required");

            RuleFor(x => x.Reservation.EndDate)
                .NotEmpty()
                .WithMessage("End date is required")
                .GreaterThan(x => x.Reservation.StartDate)
                .WithMessage("End date must be greater than start date");
            RuleFor(x => x.Car.DailyPrice).NotEmpty().WithMessage("Daily price is required.")
                          .GreaterThan(0).WithMessage("Daily price must be greater than 0.");
            RuleFor(x => x.Car.TotalCost).NotEmpty().WithMessage("Total cost is required.")
                                      .Equal(x => (x.Reservation.EndDate - x.Reservation.StartDate).TotalDays * x.Car.DailyPrice)
                                      .WithMessage("Total cost must be equal to (End date - Start date) * Daily price.");
        }
    }


}
