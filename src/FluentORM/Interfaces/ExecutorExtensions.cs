using Microsoft.Extensions.Options;

namespace FluentORM.Interfaces;

public static class ExecutorExtensions
{
    public static Task ExecuteAsync<TContext>(this IExecutor<TContext> executor, string query, params CommandParameter[] parameters)
        => executor.ExecuteAsync(query, default, parameters);

    public static IOptions<TContext> ToOptions<TContext>(this TContext context)
        where TContext : class, IExecutorContext
        => Options.Create(context);
}