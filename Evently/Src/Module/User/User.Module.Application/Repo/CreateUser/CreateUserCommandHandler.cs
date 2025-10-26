using BuildingBlock.Common.Application.Messaging;
using BuildingBlock.Common.Domain;
using User.Module.Domain;
using User.Module.Domain.Models;

namespace User.Module.Application.Repo.CreateUser;


public record CreateUserHandlerRequest(string UserName, string Email, string Address)
                                        : ICommand<CreateUserHandlerResponse>;
public record CreateUserHandlerResponse(Guid UserId);

internal sealed class CreateUserCommandHandler(IUserRepository eventRepository, IUnitOfWork unitOfWork)
                            : ICommandHandler<CreateUserHandlerRequest, CreateUserHandlerResponse>
{
    public async Task<Result<CreateUserHandlerResponse>> Handle(CreateUserHandlerRequest request, CancellationToken cancellationToken)
    {
        var @event = UserModel.Create(Guid.NewGuid(),
                            request.UserName,
                            request.Email,
                            request.Address);
        eventRepository.Insert(@event);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(new CreateUserHandlerResponse(@event.UserId));
    }
}
