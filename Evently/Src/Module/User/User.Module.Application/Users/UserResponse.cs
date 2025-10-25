namespace User.Module.Application.Users;
public record UserResponse(Guid UserId, string UserName, string Email, string Address);
