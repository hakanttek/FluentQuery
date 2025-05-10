using FluentQuery.Interfaces;

namespace FluentQuery;

public abstract class ExecutorBase : IExecutor
{
    protected IColumnMapper Mapper;

    protected ExecutorBase(IColumnMapper mapper)
    {
        Mapper = mapper;
    }

    public Task ExecuteNonQueryAsync(string query)
    {
        throw new NotImplementedException();
    }

    public Task<T?> ExecuteAsync<T>(string query)
    {
        throw new NotImplementedException();
    }
}
