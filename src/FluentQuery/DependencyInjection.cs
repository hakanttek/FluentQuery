using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery;

public static class DependencyInjection
{
    private static IServiceCollection AddStandardServices(this IServiceCollection services)
    {
        services.AddSingleton<IColumnMapper, ColumnMapper>();
        return services;
    }

    public static IServiceCollection AddFluentQuery(this IServiceCollection services, Action<ExecutorContext>? options = null)
    {
        options ??= _ => { };
        services.Configure(options);
        services.AddStandardServices();
        services.AddSingleton<IExecutor, ExecutorBase>();
        return services;
    }

    public static IServiceCollection AddFluentQuery<TContext>(this IServiceCollection services, Action<TContext>? options = null) where TContext : class, IExecutorContext
    {
        options ??= _ => { };
        services.Configure(options);
        services.AddStandardServices();
        services.AddSingleton<IExecutor<TContext>, ExecutorBase<TContext>>();
        return services;
    }
}