using System.Collections.Generic;

namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string query, CancellationToken cancellation);

    public IAsyncEnumerable<T> Execute<T>(string query, CancellationToken cancellation);
}