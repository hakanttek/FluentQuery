using FluentQuery.Interfaces;

namespace FluentQuery.InMemory;

public class InMemoryExecutor : IExecutor
{
    public Task ExecuteNonQueryAsync(string sql)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> ExecuteAsync(string query)
    {
        throw new NotImplementedException();
    }

    public Task<T> ExecuteAsync<T>(string query)
    {
        throw new NotImplementedException();
    }
}
