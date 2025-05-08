using FluentQuery.Interfaces;

namespace FluentQuery.InMemory;

public class InMemoryExecutor : IExecutor
{
    public Task ExecuteAsync(string sql)
    {
        throw new NotImplementedException();
    }

    public Task<T> ExecuteAsync<T>(string sql)
    {
        throw new NotImplementedException();
    }
}
