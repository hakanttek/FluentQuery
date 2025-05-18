using FluentQuery.Interfaces;
using System.Data.Common;

namespace FluentQuery;

public class ConnectionBuilder<TDbConnection> where TDbConnection : DbConnection, IConnectionBuilder<TDbConnection>
{
    public class Factory
    {
        public Func<TDbConnection> Create { get; init; } = ()
            => throw new NotImplementedException($"Connection factory of {typeof(TDbConnection)} is not configured.");
    }

    public ConnectionBuilder(Factory factory)
    {
        _lazyConnection = new(factory.Create);
    }

    private readonly Lazy<TDbConnection> _lazyConnection;

    public TDbConnection GetConnection() => _lazyConnection.Value;
}