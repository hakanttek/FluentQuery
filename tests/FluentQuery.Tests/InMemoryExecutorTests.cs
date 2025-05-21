using FluentQuery.InMemory;
using FluentQuery.Interfaces;
using FluentQuery.Tests.Mock;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace FluentQuery.Tests;

public class InMemoryExecutorTests
{
    private IExecutor _executor;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddFluentQuery(opt => opt.UseInMemory());

        var serviceProvider = services.BuildServiceProvider();

        _executor = serviceProvider.GetRequiredService<IExecutor>();
    }

    private async Task<bool> TryCrateTable(string sql)
    {
        try
        {
            await _executor.ExecuteAsync(sql);
            return true;
        }
        catch (SqliteException)
        {
            return false;
        }
    }

    [Test]
    public async Task ExecuteAsync_ShouldReturnExpectedUser()
    {
        // Arrange
        var createTableSql = @"
        CREATE TABLE Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FullName TEXT
        );
        INSERT INTO Users (FullName) VALUES ('John Doe');
        ";

        var selectAllSql = @"SELECT * FROM Users";

        // Act
        await TryCrateTable(createTableSql);
        var user = await _executor.Execute<User>(selectAllSql).FirstOrDefaultAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user, Is.Not.Null);
            Assert.That(user?.Id, Is.Not.Null);
            Assert.That(user?.FullName, Is.EqualTo("John Doe"));
        });
    }

    [Test]
    public async Task ExecuteAsync_WithParameter_ShouldReturnExpectedUser()
    {
        // Arrange
        var createTableSql = @"
        CREATE TABLE Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FullName TEXT
        );
        INSERT INTO Users (FullName) VALUES ('John Doe');
        ";

        var selectAllSql = @"SELECT * FROM Users WHERE FullName = @fullName";

        // Act
        await TryCrateTable(createTableSql);
        var user = await _executor.Execute<User>(selectAllSql, "John Doe".ToParam("fullName")).FirstOrDefaultAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user, Is.Not.Null);
            Assert.That(user?.Id, Is.Not.Null);
            Assert.That(user?.FullName, Is.EqualTo("John Doe"));
        });
    }
}
