namespace FluentQuery.Interfaces;

public interface IExecutor
{
    public Task ExecuteAsync<T>(string sql);
}
