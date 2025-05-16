using FluentQuery.InMemory.Interfaces;
using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;

namespace FluentQuery.InMemory;

public static class DependencyInjection
{
    public static void AddInMemoryExecutor(this IServiceCollection services)
    {
        services.AddScoped<IExecutor, InMemoryExecutor>();
        services.AddSingleton<IColumnMapper, ColumnMapper>();
        Batteries.Init();
    }
}
