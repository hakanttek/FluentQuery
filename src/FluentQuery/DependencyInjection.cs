using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery;

public static class DependencyInjection
{
    public static IServiceCollection AddFluentQuery(this IServiceCollection services, Action<ExecutorOptions>? options = null)
    {
        options ??= _ => { };
        services.Configure(options);
        services.AddSingleton<IColumnMapper, ColumnMapper>();
        services.AddSingleton<IExecutor, Executor>();
        return services;
    }
}