using BuildingBlock.Common.Application.Messaging;
using BuildingBlock.Common.Domain;
using User.Module.Domain;
using User.Module.Domain.Models;

namespace User.Module.Application.Repo.GetUserDetails;


public record GetUserByIdRequest(Guid Id) : IQuery<Result<GetUserByIdResponse>>;
public record GetUserByIdResponse(UserModel UserModel);
internal sealed class GetUserByIdQueryHandler(IUserRepo userRepo)
                : IQueryHandler<GetUserByIdRequest, Result<GetUserByIdResponse>>
{
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        UserModel result = await userRepo.GetAllUser(request.Id, cancellationToken);
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



