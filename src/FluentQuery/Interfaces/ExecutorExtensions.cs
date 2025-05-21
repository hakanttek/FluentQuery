namespace FluentQuery.Interfaces;

public static class ExecutorExtensions
{
    public static Task ExecuteAsync<TContext>(this IExecutor<TContext> executor, string query, params CommandParameter[] parameters)
        => executor.ExecuteAsync(query, default, parameters);
}
