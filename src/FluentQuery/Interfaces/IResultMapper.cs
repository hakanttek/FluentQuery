namespace FluentQuery.Interfaces;

public interface IResultMapper
{
    public IEnumerable<T> Map<T>(IDictionary<string, IEnumerable<object?>> result);
}
