
using BuildingBlock.Common.Domain;

namespace BuildingBlock.Common.Application.Exceptions;
public sealed class UsersException : Exception
{
    public UsersException(string requestName, Error? error = default, Exception? innerException = default)
            : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
