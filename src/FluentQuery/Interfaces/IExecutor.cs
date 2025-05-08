namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string sql);

    public IDictionary<string, object?> ExecuteAsync(string query);
}