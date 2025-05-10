using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery;

public static class DependencyInjection
{
    public static IServiceCollection AddFluentQuery(this IServiceCollection services, Action<ExecutorOptions>? options = null)
    {
        services.Configure(options ?? (opt => { }));
        return services;
    }
}
