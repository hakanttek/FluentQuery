using FluentQuery.InMemory;

namespace FluentQuery.Tests.Mock;

internal class MockDb : StaticExecutorContext
{
    public override void Configure(StaticExecutorContext context)
    {
        context.UseInMemory();
    }
}