<p align="center">
  <a href="http://fluent-query.hakantek.com/" target="blank"><img src="https://raw.githubusercontent.com/hakanttek/FluentQuery/e37ea2e42c27d99a8478219ee92a3873fc18c8ba/assest/icon.svg" width="120" alt="Nest Logo" /></a>
</p>

[circleci-image]: https://img.shields.io/circleci/build/github/nestjs/nest/master?token=abc123def456
[circleci-url]: https://circleci.com/gh/nestjs/nest

  <p align="center">A lightweight <a href="http://nodejs.org" target="_blank">.NET</a> library for automatically mapping SQL query results to strongly typed objects.</p>
    <p align="center">
<a href="https://www.nuget.org/packages/FluentQuery" target="_blank"><img src="https://img.shields.io/nuget/v/FluentQuery.svg?style=flat" alt="NuGet Version" /></a>
<a href="https://raw.githubusercontent.com/hakanttek/FluentQuery/refs/heads/master/LICENSE.txt" target="_blank"><img src="https://img.shields.io/github/license/hakanttek/FluentQuery" alt="Package License" /></a>
</p>

---
## 🚀 Features

- ♻️ Supports automatic mapping using `System.Data.Linq.Mapping.ColumnAttribute` over property-to-column mapping.
- 🏗️ Extensible with custom connection factory
- 🧩Multi-framework support: `.NET 6`, `.NET 7`, `.NET 8`, `.NET 9`
- 🧪 Supports both dependency injection via (`IServiceCollection`) and static usage via (`StaticExecutorContext`)
---
## 📦 Installation

Install via NuGet:

```bash
dotnet add package FluentQuery
```

Or use the Package Manager:
```bash
Install-Package FluentQuery
```
---
## 🚀 Usage Example

### Define your model
```csharp
public class User
{
  [Column("id")]
  public int Id { get; set; }

  [Column("name")]
  public string Name { get; set; }
}
```

### Register FluentQuery with Dependency Injection
##### Configure with a custom connection factory
```csharp
services.AddFluentQuery(opt => opt.ConnectionFactory = () => CreateDbConnection(cnnStr));
```
##### Alternatively, use an extension library (e.g., `FluentQuery.InMemory`)
```csharp
services.AddFluentQuery(opt => opt.UseInMemory());
```

### Execute SQL Queries
##### Without Returning Results
```csharp
await _executor.ExecuteAsync("INSERT INTO Users (FullName) VALUES ('John Doe');");
```
##### Map results to a strongly-typed model
```csharp
var query = "SELECT * FROM Users WHERE FullName = @fullName";
var user = await _executor.Execute<User>(query, "John Doe".ToParam("fullName")).FirstOrDefaultAsync();
```

### Alternatively, use static executor
##### Configure a `StaticExecutorContext`
```csharp
public class MockDb : StaticExecutorContext
{
    public override void Configure(StaticExecutorContext context)
    {
        context.UseInMemory();
    }
}
```
##### Map Results to a Strongly-Typed Model
```csharp
var query = "SELECT * FROM Users WHERE FullName = @fullName";
var user = await Executor<MockDb>.Static.Execute<User>(selectAllSql).FirstOrDefaultAsync();
```
---
## 📬 Stay in touch
- Author - [Hakan Tek](https://www.hakantek.com/)
- Website - [flow-query.hakantek.com](https://flow-query.hakantek.com/)
---
## 📋 License
FlowQuery is [MIT licensed](https://raw.githubusercontent.com/hakanttek/FluentQuery/refs/heads/master/LICENSE.txt)