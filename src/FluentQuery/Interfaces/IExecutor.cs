namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string query);

    public Task<T?> ExecuteAsync<T>(string query);
}