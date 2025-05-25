using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentQuery;

public static class DependencyInjection
{
    private static IServiceCollection AddStandardServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IColumnMapper, ColumnMapper>();
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
        if (typeof(TContext) == typeof(ExecutorContext))
            throw new ArgumentException("The generic overload AddFluentQuery<TContext> cannot be used with ExecutorContext. Please use the non-generic AddFluentQuery() method instead.", nameof(TContext));
        options ??= _ => { };
        services.Configure(options);
        services.AddStandardServices();
        services.AddSingleton<IExecutor<TContext>, ExecutorBase<TContext>>();
        return services;
    }

    internal static IServiceCollection AddStaticFluentQuery(this IServiceCollection services)
    {
        services.AddStandardServices();
        services.TryAdd(ServiceDescriptor.Singleton(typeof(IStaticExecutor<>), typeof(StaticExecutor<>)));
        return services;
    }
}