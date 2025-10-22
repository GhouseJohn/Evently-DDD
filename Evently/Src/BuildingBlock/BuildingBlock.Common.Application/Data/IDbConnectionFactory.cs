using System.Data.Common;

namespace BuildingBlock.Common.Application.Data;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
