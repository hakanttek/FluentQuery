namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string sql);

    public Task<dynamic> ExecuteAsync(string query);

    public Task<T> ExecuteAsync<T>(string query);
}