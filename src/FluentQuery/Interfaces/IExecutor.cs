namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteNonQueryAsync(string query);

    public Task<IEnumerable<T>> ExecuteAsync<T>(string query);
}