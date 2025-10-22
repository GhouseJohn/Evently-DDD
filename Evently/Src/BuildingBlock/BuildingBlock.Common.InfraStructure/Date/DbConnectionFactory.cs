using System.Data.Common;
using BuildingBlock.Common.Application.Data;
using Npgsql;

namespace BuildingBlock.Common.InfraStructure.Date;
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
