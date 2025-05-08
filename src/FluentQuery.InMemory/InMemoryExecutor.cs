using FluentQuery.Interfaces;

namespace FluentQuery.InMemory;

public class InMemoryExecutor : IExecutor
{
    public IDictionary<string, object?> ExecuteAsync(string query)
    {
        throw new NotImplementedException();
    }

    public Task ExecuteNonQueryAsync(string sql)
    {
        throw new NotImplementedException();
    }
}
