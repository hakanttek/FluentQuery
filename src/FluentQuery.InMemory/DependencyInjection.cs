using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery.InMemory;

public static class DependencyInjection
{
    public static void UseInMemory(this ExecutorOptions options)
    {
        options.Use<InMemoryExecutor>(ServiceLifetime.Singleton);
    }
}
