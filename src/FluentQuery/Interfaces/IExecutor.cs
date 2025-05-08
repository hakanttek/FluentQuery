namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string sql);

    public Task<IDictionary<string, object?>> ExecuteAsync(string query);
}