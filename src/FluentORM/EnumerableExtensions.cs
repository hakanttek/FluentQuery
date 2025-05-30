namespace FluentORM;

public static class EnumerableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        var result = new List<T>();
        await foreach (var item in source)
        {
            result.Add(item);
        }
        return result;
    }

    public static async Task<T?> FirstOrDefaultAsync<T>(this IAsyncEnumerable<T> source)
    {
        await foreach (var item in source)
        {
            return item;
        }
        return default;
    }

    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if (!await enumerator.MoveNextAsync())
            return default;

        T result = enumerator.Current;

        if (await enumerator.MoveNextAsync())
            throw new InvalidOperationException("Sequence contains more than one element");

        return result;
    }
    
    public static async Task<T> FirstAsync<T>(this IAsyncEnumerable<T> source)
    {
        await foreach (var item in source)
        {
            return item;
        }
        throw new InvalidOperationException("Sequence contains no elements");
    }

    public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if (!await enumerator.MoveNextAsync())
            throw new InvalidOperationException("Sequence contains no elements");

        T result = enumerator.Current;

        if (await enumerator.MoveNextAsync())
            throw new InvalidOperationException("Sequence contains more than one element");

        return result;
    }
}