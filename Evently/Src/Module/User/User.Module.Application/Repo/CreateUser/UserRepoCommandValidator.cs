using FluentValidation;

namespace User.Module.Application.Repo.CreateUser;
internal sealed class UserRepoCommandValidator : AbstractValidator<CreateUserHandlerRequest>
{
    public UserRepoCommandValidator()
    {
        RuleFor(u => u.UserName).NotEmpty().MaximumLength(5).MinimumLength(3);
        RuleFor(u => u.Address).NotEmpty();
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
    }
}
