namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string query);

    public Task<IDictionary<string, IEnumerable<object?>>> ExecuteAsync(string query);
}