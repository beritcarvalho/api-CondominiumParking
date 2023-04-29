using CondominiumParkingApi.Applications.InputModels;
using FluentValidation;

namespace CondominiumParkingApi.Applications.Validators
{
    public class ChangeParkingSpaceAvailabilityValidator : AbstractValidator<ChangeParkingSpaceAvailability>
    {
        public ChangeParkingSpaceAvailabilityValidator()
        {
            Include(new RangeInputModelValidator());

            RuleFor(spaces => spaces.Active)
                .NotNull();
        }
    }
}
