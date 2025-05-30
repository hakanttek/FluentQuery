using System.Data.Common;

namespace FluentORM;

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
}