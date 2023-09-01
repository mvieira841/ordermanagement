namespace OrderManagement.Application.Features.Auth.Commands.RegisterUser;

internal class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required")
            .EmailAddress()
            .WithMessage("A valid email address for the UserName is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");

        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role is required");
    }
}