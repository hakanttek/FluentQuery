<p align="center">
  <a href="http://fluent-orm.hakantek.com/" target="blank"><img src="https://raw.githubusercontent.com/hakanttek/FluentORM/e37ea2e42c27d99a8478219ee92a3873fc18c8ba/assest/icon.svg" width="120" alt="FluentORM Logo" /></a>
</p>
  <p align="center">A lightweight <a href="http://nodejs.org" target="_blank">.NET</a> library for automatically mapping SQL query results to strongly typed objects.</p>
    <p align="center">
<a href="https://www.nuget.org/packages/FluentORM" target="_blank"><img src="https://img.shields.io/nuget/v/FluentORM.svg?style=flat" alt="NuGet Version" /></a>
<a href="https://raw.githubusercontent.com/hakanttek/FluentORM/refs/heads/master/LICENSE.txt" target="_blank"><img src="https://img.shields.io/github/license/hakanttek/FluentORM" alt="Package License" /></a>
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
dotnet add package FluentORM
```

Or use the Package Manager:
```bash
Install-Package FluentORM
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

### Register FluentORM with Dependency Injection
##### Configure with a custom connection factory
```csharp
services.AddFluentORM(opt => opt.ConnectionFactory = () => CreateDbConnection(cnnStr));
```
##### Alternatively, use an extension library (e.g., `FluentORM.InMemory`)
```csharp
services.AddFluentORM(opt => opt.UseInMemory());
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

### Alternatively, use a static executor
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
##### Execute query via Executor<TContext>.Static
```csharp
var query = "SELECT * FROM Users WHERE FullName = @fullName";
var user = await Executor<MockDb>.Static.Execute<User>(selectAllSql).FirstOrDefaultAsync();
```
---
## 📬 Stay in touch
- Author - [Hakan Tek](https://www.hakantek.com/)
- Website - [fluent-orm.hakantek.com](https://fluent-orm.hakantek.com/)
---
## 📋 License
FluentORM is [MIT licensed](https://raw.githubusercontent.com/hakanttek/FluentORM/refs/heads/master/LICENSE.txt)