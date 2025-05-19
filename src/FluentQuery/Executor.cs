using FluentQuery.Interfaces;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace FluentQuery;

public class Executor : IExecutor
{
    private readonly IColumnMapper _mapper;

    private readonly ExecutorContext _options;

    public Executor(IColumnMapper mapper, IOptions<ExecutorContext> options)
    {
        _mapper = mapper;
        _options = options.Value;
    }

    public async Task ExecuteNonQueryAsync(string query, CancellationToken cancellation = default)
    {
        using var connection = await _options.ConnectionFactory.OpenConnectionAsync(cancellation);

        using var command = connection.CreateCommand();

        command.CommandText = query;

        await command.ExecuteNonQueryAsync(cancellation);

        await connection.CloseAsync();
    }

    public async IAsyncEnumerable<T> Execute<T>(string query, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        using var connection = await _options.ConnectionFactory.OpenConnectionAsync(cancellation);

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