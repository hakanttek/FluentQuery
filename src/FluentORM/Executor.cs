using FluentORM.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentORM;

public static class Executor<TContext> where TContext : StaticExecutorContext, IExecutorContext, new()
{
    private static readonly IServiceCollection Services = new ServiceCollection().AddStaticFluentORM();

    private static readonly Lazy<IServiceProvider> LazyProvider = new(Services.BuildServiceProvider);

    private static IServiceProvider ServiceProvider => LazyProvider.Value;

    public static IExecutor<TContext> Static => ServiceProvider.GetRequiredService<IStaticExecutor<TContext>>();
}