using CondominiumParkingApi.Applications.InputModels;
using FluentValidation;

namespace CondominiumParkingApi.Applications.Validators
{
    public class RangeInputModelValidator : AbstractValidator<RangeInputModel>
    {
        public RangeInputModelValidator()
        {
            RuleFor(range => range.From)
                .NotNull()
                .GreaterThan(0).WithMessage("O índice inicial precisa ser maior que 0");

            RuleFor(range => range.To)
                .NotNull()
                .GreaterThan(0).WithMessage("O índice final precisa ser maior que 0");

            RuleFor(range => new { range.From, range.To})
                .NotNull()
                .Must(range => range.From <= range.To).WithMessage("O índice inicial tem menor ou igual ao índice final");
        }
    }
}
