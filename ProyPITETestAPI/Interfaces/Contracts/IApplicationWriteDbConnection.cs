using System.Data;

namespace Proyecto.PiteApi.Interfaces.Contracts;

public interface IApplicationWriteDbConnection : IApplicationReadDbConnection
{
    Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
}
