using FluentQuery.Interfaces;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace FluentQuery.InMemory;

public static class DependencyInjection
{
    private static readonly string InMemoryDbName = Guid.NewGuid().ToString("N") + "Db";

    public static readonly ConnectionFactory InMemory = new(()
        => new SqliteConnection($"Data Source=file:{InMemoryDbName}?mode=memory&cache=shared"));

    public static void UseInMemory(this ExecutorContext context)
    {
        context.ConnectionFactory  = InMemory;
        Batteries.Init();
    }
}