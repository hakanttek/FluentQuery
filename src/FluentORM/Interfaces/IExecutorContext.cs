namespace FluentORM.Interfaces;

public interface IExecutorContext
{
    public ConnectionFactory ConnectionFactory { get; }
}