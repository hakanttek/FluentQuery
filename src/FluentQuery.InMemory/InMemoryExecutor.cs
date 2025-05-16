using FluentQuery.InMemory.Interfaces;
using FluentQuery.Interfaces;
using Microsoft.Data.Sqlite;
using System.Runtime.CompilerServices;

namespace FluentQuery.InMemory;

public class InMemoryExecutor : IExecutor
{
    private readonly IColumnMapper _mapper;

    public InMemoryExecutor(IColumnMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task ExecuteNonQueryAsync(string query, CancellationToken cancellation = default)
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync(cancellation);

        using var command = connection.CreateCommand();
        command.CommandText = query;

        await command.ExecuteNonQueryAsync(cancellation);
    }

    public async IAsyncEnumerable<T> Execute<T>(string query, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync(cancellation);

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = await command.ExecuteReaderAsync(cancellation = default);

        while (await reader.ReadAsync(cancellation))
        {
            yield return _mapper.Map<T>(reader);
        }
    }
}
