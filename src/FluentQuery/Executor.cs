using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery;

public static class Executor<TContext> where TContext : StaticExecutorContext, IExecutorContext, new()
{
    private static readonly IServiceCollection Services = new ServiceCollection().AddStaticFluentQuery();

    private static readonly Lazy<IServiceProvider> LazyProvider = new(Services.BuildServiceProvider);

    private static IServiceProvider ServiceProvider => LazyProvider.Value;

    public static IExecutor<TContext> Static => ServiceProvider.GetRequiredService<IStaticExecutor<TContext>>();
}