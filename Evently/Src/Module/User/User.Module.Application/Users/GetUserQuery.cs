using BuildingBlock.Common.Application.Messaging;

namespace User.Module.Application.Users;
public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
