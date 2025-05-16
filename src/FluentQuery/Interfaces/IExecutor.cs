using System.Collections.Generic;

namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string query);

    public IAsyncEnumerable<T> ExecuteAsync<T>(string query);
}