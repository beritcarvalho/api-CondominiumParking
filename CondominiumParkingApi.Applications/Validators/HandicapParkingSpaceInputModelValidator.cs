using CondominiumParkingApi.Applications.InputModels;
using FluentValidation;

namespace CondominiumParkingApi.Applications.Validators
{
    public class HandicapParkingSpaceInputModelValidator : AbstractValidator<HandicapParkingSpaceInputModel>
    {
        public HandicapParkingSpaceInputModelValidator()
        {
            Include(new RangeInputModelValidator());

            RuleFor(spaces => spaces.Handicap)
                .NotNull();
        }
    }
}
