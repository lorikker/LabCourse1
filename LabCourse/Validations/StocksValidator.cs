using FluentValidation;
using LabCourse.Entities;

namespace LabCourse.Validations;

public class StokuValidator : AbstractValidator<Stoku>
{
    public StokuValidator()
    {
        RuleFor(s => s.EmriStokut).NotEmpty().WithMessage("Stock name is required.");
        RuleFor(s => s.LlojiProduktit).NotEmpty().WithMessage("Product type is required.");
        RuleFor(s => s.Sasia).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(s => s.Location).NotEmpty().WithMessage("Location is required.");
        RuleFor(s => s.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than zero.");
    }
}