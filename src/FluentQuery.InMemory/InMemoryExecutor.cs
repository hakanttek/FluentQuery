using FluentQuery.Interfaces;

namespace FluentQuery.InMemory;

public class InMemoryExecutor : IExecutor
{
    public Task ExecuteAsync<T>(string sql)
    {
        throw new NotImplementedException();
    }
}
