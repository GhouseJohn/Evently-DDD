using Microsoft.AspNetCore.Routing;

namespace BuildingBlock.Common.Presentation.Endpoints;
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

