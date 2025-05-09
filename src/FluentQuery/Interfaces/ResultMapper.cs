namespace FluentQuery.Interfaces;

public interface ResultMapper<T>
{
    public IEnumerable<T> Map(IDictionary<string, IEnumerable<object?>> result);
}
