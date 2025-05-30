namespace FluentORM.Interfaces;

public interface IExecutor<TContext>
{
    public Task ExecuteAsync(string query, CancellationToken cancellation = default, params CommandParameter[] parameters);

    public IAsyncEnumerable<T> Execute<T>(string query, CancellationToken cancellation = default, params CommandParameter[] parameters);

    public IAsyncEnumerable<T> Execute<T>(string query, params CommandParameter[] parameters) => Execute<T>(query, default, parameters);
}

public interface IExecutor : IExecutor<ExecutorContext>
{
}

public interface IStaticExecutor<TContext> : IExecutor<TContext> where TContext : StaticExecutorContext
{
}