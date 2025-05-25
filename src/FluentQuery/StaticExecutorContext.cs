using FluentQuery.Interfaces;

namespace FluentQuery;

public abstract class StaticExecutorContext : ExecutorContext, IExecutorContext
{
    public abstract void Configure(StaticExecutorContext context);

    internal static TContext Create<TContext>() where TContext : StaticExecutorContext, new()
    {
        var context = new TContext();
        context.Configure(context);
        return context;
    }
}