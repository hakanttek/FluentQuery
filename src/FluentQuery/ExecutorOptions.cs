using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery;

public class ExecutorOptions
{
    public ExecutorOptions(IServiceCollection services)
    {
        Services = services;
    }

    protected readonly IServiceCollection Services;

    public string ConnectionString { get; set; } = string.Empty;

    public void Use<TExecutor>(ServiceLifetime serviceLifetime) where TExecutor : class, IExecutor
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Transient:
                Services.AddTransient<IExecutor, TExecutor>();
                break;
            case ServiceLifetime.Scoped:
                Services.AddScoped<IExecutor, TExecutor>();
                break;
            case ServiceLifetime.Singleton:
                Services.AddSingleton<IExecutor, TExecutor>();
                break;
        }
    }
}
