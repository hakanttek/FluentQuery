using FluentORM.Interfaces;

namespace FluentORM;

public class ExecutorContext : IExecutorContext
{
#if NET7_0_OR_GREATER
    public virtual ConnectionFactory ConnectionFactory { get; set; } = new ConnectionFactory(()
        => throw new NotSupportedException("A ConnectionFactory has not been configured for the StaticExecutorContext. Please provide a valid ConnectionFactory instance before attempting to use this context.")
    );
#else
    public virtual ConnectionFactory ConnectionFactory { get; set; } = null!;
#endif
}