using FluentQuery.Interfaces;

namespace FluentQuery.InMemory;

public class InMemoryExecutor : IExecutor
{
    public Task ExecuteNonQueryAsync(string query)
    {
        throw new NotImplementedException();
    }

    public Task<IDictionary<string, IEnumerable<object?>>> ExecuteAsync(string query)
    {
        throw new NotImplementedException();
    }
}
