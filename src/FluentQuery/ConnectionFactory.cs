using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace FluentQuery;

public class ConnectionFactory<TDbConnection> where TDbConnection : DbConnection
{
    private readonly Func<TDbConnection> _factory;

    public ConnectionFactory(Func<TDbConnection> factory)
    {
        _factory = factory;
    }
        
    public TDbConnection Create() => _factory.Invoke();

    public async Task<TDbConnection> OpenConnectionAsync(CancellationToken cancellation = default)
    {
        var connection = Create();
        await connection.OpenAsync(cancellation);
        return connection;
    }
}

public class ConnectionFactory : ConnectionFactory<DbConnection>
{
    public ConnectionFactory(Func<DbConnection> factory) : base(factory)
    {
    }

    private static readonly string InMemoryDbName = Guid.NewGuid().ToString("N") + "Db";

    public static readonly ConnectionFactory InMemory = new(()
        => new SqliteConnection($"Data Source=file:{InMemoryDbName}?mode=memory&cache=shared"));
}