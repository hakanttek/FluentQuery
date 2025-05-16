using FluentQuery.InMemory;
using FluentQuery.Interfaces;
using FluentQuery.Tests.Mock;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentQuery.Tests;

public class InMemoryExecutorTests
{
    private IExecutor _executor;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddFluentQuery();

        services.AddInMemoryExecutor();

        var serviceProvider = services.BuildServiceProvider();

        _executor = serviceProvider.GetRequiredService<IExecutor>();
    }

    [TestCase(@"
        CREATE TABLE Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FullName TEXT
        );
        INSERT INTO Users (FullName) VALUES ('John Doe');
        ",
        "SELECT * FROM Users",
        "FullName",
        "John Doe",
        TestName = "UserTable")]
    public async Task ExecuteAsync_ShouldCreateTable_InsertEntity_AndReturnExpectedResult(
        string createTableSql,
        string selectAllSql,
        string key,
        string value)
    {
        // Act
        await _executor.ExecuteNonQueryAsync(createTableSql);
        var users = await _executor.ExecuteAsync<User>(selectAllSql).ToListAsync();
        var user = users.FirstOrDefault();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user, Is.Not.Null);
            Assert.That(user?.Id, Is.Not.Null);
            Assert.That(user?.FullName, Is.EqualTo("John Doe"));
        });
    }
}
