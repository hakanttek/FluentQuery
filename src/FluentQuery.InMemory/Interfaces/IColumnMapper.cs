namespace FluentQuery.InMemory.Interfaces;

public interface IColumnMapper
{
    public T Map<T>(IDictionary<string, object?> result);
}
