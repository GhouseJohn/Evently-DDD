using FluentValidation;

namespace User.Module.Application.Repo.CreateUser;
public sealed class UserRepoCommandValidation : AbstractValidator<CreateUserHandlerRequest>
{
    public UserRepoCommandValidation()
    {
        RuleFor(u => u.UserName).NotEmpty().MaximumLength(5).MinimumLength(3);
        RuleFor(u => u.Address).NotEmpty();
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
    }
}
