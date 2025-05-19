using FluentQuery.Interfaces;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace FluentQuery;

public class ExecutorBase<TContext> : IExecutor<TContext> where TContext : ExecutorContext
{
    protected readonly IColumnMapper Mapper;

    protected readonly TContext Context;

    public ExecutorBase(IColumnMapper mapper, IOptions<TContext> contextOptions)
    {
        Mapper = mapper;
        Context = contextOptions.Value;
    }

    public async virtual Task ExecuteAsync(string query, CancellationToken cancellation = default)
    {
        using var connection = await Context.ConnectionFactory.OpenConnectionAsync(cancellation);

        using var command = connection.CreateCommand();

        command.CommandText = query;

        await command.ExecuteNonQueryAsync(cancellation);

        await connection.CloseAsync();
    }

    public async virtual IAsyncEnumerable<T> Execute<T>(string query, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        using var connection = await Context.ConnectionFactory.OpenConnectionAsync(cancellation);

        using var command = connection.CreateCommand();

        command.CommandText = query;

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