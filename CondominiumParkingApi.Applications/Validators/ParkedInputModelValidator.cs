using CondominiumParkingApi.Applications.InputModels;
using FluentValidation;

namespace CondominiumParkingApi.Applications.Validators
{
    public class ParkedInputModelValidator : AbstractValidator<ParkedInputModel>
    {
        public ParkedInputModelValidator()
        {
            RuleFor(parked => parked.ParkingSpaceId)
                .NotNull()
                .GreaterThan(0).WithMessage("O ID da vaga deve ser maior que 0");

            RuleFor(parked => parked.VehicleId)
                .NotNull()
                .GreaterThan(0).WithMessage("O ID do veículo deve ser maior que 0");
        }
    }
}
