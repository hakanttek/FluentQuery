using FluentQuery.Interfaces;

namespace FluentQuery;

public abstract class StaticExecutorContext : ExecutorContext, IExecutorContext
{
    public abstract void Configure(StaticExecutorContext context);
}