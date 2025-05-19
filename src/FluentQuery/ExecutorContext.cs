using FluentQuery.Interfaces;

namespace FluentQuery;

public class ExecutorContext : IExecutorContext
{
    public required ConnectionFactory ConnectionFactory { get; set; }
}