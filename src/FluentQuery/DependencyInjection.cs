using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery;

public static class DependencyInjection
{
    public static IServiceCollection AddFluentQuery(this IServiceCollection services, Action<ExecutorOptions> setupAction)
    {
        var options = new ExecutorOptions(services);
        setupAction(options);

        if(!options.ExecutorRegistered)
            throw new InvalidOperationException("Executor not registered.");

        return services.AddSingleton(options);
    }
}
