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

    public async Task<IEnumerable<T>> ExecuteAsync<T>(string query)
    {
        var results = new List<T>();

        using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(_mapper.Map<T>(reader));
        }

        return results;
    }
}
