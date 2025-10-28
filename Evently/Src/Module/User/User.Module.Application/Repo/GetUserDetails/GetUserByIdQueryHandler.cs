using BuildingBlock.Common.Application.Messaging;
using BuildingBlock.Common.Domain;
using User.Module.Domain;
using User.Module.Domain.Models;

namespace User.Module.Application.Repo.GetUserDetails;


public sealed record GetUserByIdRequest(Guid Id) : IQuery<GetUserByIdResponse>;
public sealed record GetUserByIdResponse(UserModel UserModel);
internal sealed class GetUserByIdQueryHandler(IUserRepository userRepo)
                : IQueryHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        UserModel result = await userRepo.GetAsync(request.Id, cancellationToken);
        if (result is null)
        {
            return Result.Failure<GetUserByIdResponse>(UserErrors.NotFound(request.Id));
        }
        else
        {
            return Result.Success(new GetUserByIdResponse(result));
        }
    }
}



