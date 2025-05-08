namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string sql);

    public Task<IDictionary<string, IEnumerable<object?>>> ExecuteAsync(string query);
}