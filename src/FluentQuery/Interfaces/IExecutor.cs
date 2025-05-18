namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string query, CancellationToken cancellation = default);

    public IAsyncEnumerable<T> Execute<T>(string query, CancellationToken cancellation = default);
}

public interface IExecutor<TExecutorOptions> : IExecutor where TExecutorOptions : ExecutorOptions
{
}