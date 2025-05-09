using FluentQuery.Interfaces;

namespace FluentQuery;

public abstract class ExecutorBase : IExecutor
{
    protected IResultMapper Mapper;

    protected ExecutorBase(IResultMapper mapper)
    {
        Mapper = mapper;
    }

    public abstract Task<IDictionary<string, IEnumerable<object?>>> ExecuteAsync(string query);

    public abstract Task ExecuteNonQueryAsync(string query);
}
