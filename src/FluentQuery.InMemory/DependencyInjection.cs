using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;

namespace FluentQuery.InMemory;

public static class DependencyInjection
{
    public static void UseInMemory(this ExecutorOptions options)
    {
        options.Use<InMemoryExecutor>(ServiceLifetime.Singleton);
        Batteries.Init();
    }
}
