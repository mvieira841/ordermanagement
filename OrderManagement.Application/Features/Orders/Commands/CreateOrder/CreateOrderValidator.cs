namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer Id is required");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product Id is required");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}