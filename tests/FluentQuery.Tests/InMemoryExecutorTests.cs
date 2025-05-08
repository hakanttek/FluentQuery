using FluentQuery.InMemory;
using FluentQuery.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery.Tests;

public class InMemoryExecutorTests
{
    private IExecutor _executor;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddFluentQuery(options =>
        {
            options.UseInMemory();
        });

        var serviceProvider = services.BuildServiceProvider();

        _executor = serviceProvider.GetRequiredService<IExecutor>();
    }

    [Test]
    public async Task Test1()
    {
        // Create a table and insert a record
        await _executor.ExecuteAsync(@"
        CREATE TABLE Users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            full_name TEXT
        );
        INSERT INTO Users (full_name) VALUES ('John Doe');
        ");

        // Select the record
        var result = await _executor.ExecuteAsync<int>("SELECT COUNT(*) FROM Users");
    }
}
