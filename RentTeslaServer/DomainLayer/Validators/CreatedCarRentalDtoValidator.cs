using FluentValidation;
using RentTeslaServer.DomainLayer.ModelDtos;

namespace RentTeslaServer.DomainLayer.Validators
{
    public class CreatedCarRentalDtoValidator:AbstractValidator<CreatedCarRentalDto>
    {
        public CreatedCarRentalDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Name is required.");
            RuleFor(x => x.City).NotEmpty().MaximumLength(50).WithMessage("City  is required.");
            RuleFor(x => x.Street).NotEmpty().MaximumLength(50).WithMessage("Street  is required.");
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(10).WithMessage("Postal code  is required.");
        }
    }
}
