using FluentORM.InMemory;

namespace FluentORM.Tests.Mock;

internal class MockDb : StaticExecutorContext
{
    public override void Configure(StaticExecutorContext context)
    {
        context.UseInMemory();
    }
}