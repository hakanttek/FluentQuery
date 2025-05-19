using FluentQuery.Interfaces;

namespace FluentQuery;

public class ExecutorContext : IExecutorContext
{
    public ConnectionFactory ConnectionFactory { get; set; } = ConnectionFactory.InMemory;
}