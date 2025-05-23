using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery;

public static class Executor<TContext> where TContext : ExecutorContext
{
    private static readonly IServiceCollection Services = new ServiceCollection().AddFluentQuery();

    private static readonly Lazy<IServiceProvider> LazyProvider = new(Services.BuildServiceProvider);

    private static IServiceProvider ServiceProvider => LazyProvider.Value;

    public static IExecutor<TContext> Static => ServiceProvider.GetRequiredService<IExecutor<TContext>>();
}