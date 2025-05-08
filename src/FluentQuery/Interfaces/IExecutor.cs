namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteAsync(string sql);

    public Task<T> ExecuteAsync<T>(string sql);
}
