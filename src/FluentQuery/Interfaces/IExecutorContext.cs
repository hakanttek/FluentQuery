namespace FluentQuery.Interfaces;

public interface IExecutorContext
{
    public ConnectionFactory ConnectionFactory { get; }
}