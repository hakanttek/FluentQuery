using FluentQuery.Interfaces;

namespace FluentQuery;

public class ExecutorContext : IExecutorContext
{
#if NET7_0_OR_GREATER
    public required ConnectionFactory ConnectionFactory { get; set; }
#else
    public ConnectionFactory ConnectionFactory { get; set; } = null!;
#endif
}