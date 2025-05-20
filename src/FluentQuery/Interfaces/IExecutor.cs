namespace FluentQuery.Interfaces;

public interface IExecutor<TContext>
{
    public Task ExecuteAsync(string query, CancellationToken cancellation = default, params CommandParameter[] parameters);

    public IAsyncEnumerable<T> Execute<T>(string query, CancellationToken cancellation = default, params CommandParameter[] parameters);
}

public interface IExecutor : IExecutor<ExecutorContext>
{
}