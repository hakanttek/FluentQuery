using FluentORM.Interfaces;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace FluentORM;

public class ExecutorBase<TContext> : IExecutor<TContext> where TContext : class, IExecutorContext
{
    protected readonly IColumnMapper Mapper;

    protected readonly TContext Context;

    public ExecutorBase(IColumnMapper mapper, IOptions<TContext> contextOptions)
    {
        Mapper = mapper;
        Context = contextOptions.Value;
    }

    public async virtual Task ExecuteAsync(string query, CancellationToken cancellation = default, params CommandParameter[] parameters)
    {
        using var connection = await Context.ConnectionFactory.OpenConnectionAsync(cancellation);

        using var command = connection.CreateCommand();

        command.CommandText = query;

        command.AddParameter(parameters);

        await command.ExecuteNonQueryAsync(cancellation);

        await connection.CloseAsync();
    }

    public async virtual IAsyncEnumerable<T> Execute<T>(string query, [EnumeratorCancellation] CancellationToken cancellation = default, params CommandParameter[] parameters)
    {
        using var connection = await Context.ConnectionFactory.OpenConnectionAsync(cancellation);

        using var command = connection.CreateCommand();

        command.CommandText = query;

        command.AddParameter(parameters);

        using var reader = await command.ExecuteReaderAsync(cancellation);

        while (await reader.ReadAsync(cancellation))
        {
            yield return Mapper.Map<T>(reader);
        }

        await connection.CloseAsync();
    }
}

public class ExecutorBase : ExecutorBase<ExecutorContext>, IExecutor<ExecutorContext>, IExecutor
{
    public ExecutorBase(IColumnMapper mapper, IOptions<ExecutorContext> options) : base(mapper, options)
    {
    }
}

public sealed class StaticExecutor<TContext> : ExecutorBase<TContext>, IStaticExecutor<TContext> where TContext : StaticExecutorContext, new()
{
    public StaticExecutor(IColumnMapper mapper) : base(mapper, StaticExecutorContext.Create<TContext>().ToOptions())
    {
        
    }
}