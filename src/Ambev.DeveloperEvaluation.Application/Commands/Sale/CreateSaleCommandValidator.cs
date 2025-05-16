using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("SaleNumber is required");
        RuleFor(x => x.Customer).NotEmpty().WithMessage("Customer is required");
        RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required");
        RuleFor(x => x.Items).NotEmpty().WithMessage("Items is required");

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.Product).NotEmpty().WithMessage("Product is required");
            items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("QQuantity must be > 0");
            items.RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("UnitPrice must be > 0");
        });
    }

}