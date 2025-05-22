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

    [Test]
    public async Task ExecuteAsync_WithJoin_ShouldReturnUserRoleWithUser()
    {
        // Arrange
        var createTableSql = @"
        CREATE TABLE Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FullName TEXT
        );

        CREATE TABLE UserRoles (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId INTEGER,
            Role TEXT,
            FOREIGN KEY (UserId) REFERENCES Users(Id)
        );

        INSERT INTO Users (FullName) VALUES ('John Doe');

        INSERT INTO UserRoles (UserId, Role)
        VALUES (
            (SELECT Id FROM Users WHERE FullName = 'John Doe'),
            'admin'
        );
        ";

        var selectAllSql = @"
        SELECT R.*, U.*
        FROM  UserRoles R
        JOIN Users U ON R.UserId = U.Id
        WHERE U.FullName = @fullName';
        ";

        // Act
        await TryCrateTable(createTableSql);
        var userRole = await _executor.Execute<UserRole>(selectAllSql, "John Doe".ToParam("fullName")).FirstOrDefaultAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(userRole, Is.Not.Null);
            Assert.That(userRole?.User, Is.Not.Null);
            Assert.That(userRole?.User?.FullName, Is.EqualTo("John Doe"));
        });
    }
}
