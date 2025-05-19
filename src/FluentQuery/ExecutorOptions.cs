namespace FluentQuery;

public class ExecutorOptions
{
    public ConnectionFactory ConnectionFactory { get; set; } = ConnectionFactory.InMemory;
}