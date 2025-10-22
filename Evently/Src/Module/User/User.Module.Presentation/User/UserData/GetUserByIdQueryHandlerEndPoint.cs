using BuildingBlock.Common.Domain;
using BuildingBlock.Common.Presentation.ApiResult;
using BuildingBlock.Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Module.Application.Repo.GetUserDetails;

namespace User.Module.Presentation.User.UserData;
internal sealed class GetUserByIdQueryHandlerEndPoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/user/{guid:guid}", async (ISender sender, Guid guid) =>
        {
            Result<GetUserByIdResponse> @result =
                                    await sender.Send(new GetUserByIdRequest(guid));
            return @result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.Users);
    }
}
