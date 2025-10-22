using BuildingBlock.Common.Domain;
using BuildingBlock.Common.Presentation.ApiResult;
using BuildingBlock.Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Module.Application.Repo.CreateUser;

namespace User.Module.Presentation.User.Create;
internal sealed class CreateUserCommandHandlerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (UserRequestDTo requestDTo, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<CreateUserHandlerResponse> result = await sender.Send(new CreateUserHandlerRequest(requestDTo.UserName,
                                                     requestDTo.Email,
                                                     requestDTo.Address), cancellationToken);
            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}

internal sealed class UserRequestDTo
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

