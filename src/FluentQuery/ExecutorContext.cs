namespace FluentQuery;

public class ExecutorContext
{
    public ConnectionFactory ConnectionFactory { get; set; } = ConnectionFactory.InMemory;
}