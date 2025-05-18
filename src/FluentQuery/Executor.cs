using FluentQuery.Interfaces;
using System.Runtime.CompilerServices;
using System.Data.Common;

namespace FluentQuery;

public class Executor<TDbConnection> : IExecutor where TDbConnection : DbConnection
{
    private readonly IColumnMapper _mapper;

    private readonly IConnectionBuilder<TDbConnection> _cnnBuilder;

    public Executor(IColumnMapper mapper, IConnectionBuilder<TDbConnection> connectionBuilder)
    {
        _mapper = mapper;
        _cnnBuilder = connectionBuilder;
    }

    public async Task ExecuteNonQueryAsync(string query, CancellationToken cancellation = default)
    {
        var connection = _cnnBuilder.GetConnection();

        await connection.OpenAsync(cancellation);

        using var command = connection.CreateCommand();
        command.CommandText = query;

        await command.ExecuteNonQueryAsync(cancellation);

        await connection.CloseAsync();
    }

    public async IAsyncEnumerable<T> Execute<T>(string query, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        using var connection = _cnnBuilder.GetConnection();

        await connection.OpenAsync(cancellation);

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = await command.ExecuteReaderAsync(cancellation);

        while (await reader.ReadAsync(cancellation))
        {
            yield return _mapper.Map<T>(reader);
        }

        await connection.CloseAsync();
    }
}
