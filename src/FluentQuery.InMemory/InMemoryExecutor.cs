using FluentQuery.InMemory.Interfaces;
using FluentQuery.Interfaces;
using Microsoft.Data.Sqlite;

namespace FluentQuery.InMemory;

public class InMemoryExecutor : IExecutor
{
    private readonly IColumnMapper _mapper;

    public InMemoryExecutor(IColumnMapper mapper)
    {
        _mapper = mapper;
    }

    public Task ExecuteNonQueryAsync(string query)
    {
        throw new NotImplementedException();
    }

    public async IAsyncEnumerable<T> ExecuteAsync<T>(string query)
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var obj = _mapper.Map<T>(reader);
            if (obj != null)
                yield return obj;
        }
    }
}
