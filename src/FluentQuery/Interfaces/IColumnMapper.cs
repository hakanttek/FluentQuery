namespace FluentQuery.Interfaces;

public interface IColumnMapper
{
    public T Map<T>(IDictionary<string, object?> result);
}
