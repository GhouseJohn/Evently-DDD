namespace BuildingBlock.Common.Application.Clock;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}

