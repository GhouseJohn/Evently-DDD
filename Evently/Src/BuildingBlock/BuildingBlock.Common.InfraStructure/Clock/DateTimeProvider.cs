using BuildingBlock.Common.Application.Clock;

namespace BuildingBlock.Common.InfraStructure.Clock;
internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
