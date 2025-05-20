using System.Data;

namespace FluentQuery;

public record CommandParameter<T>
{
    public readonly string Name;

    public readonly T? Value;

    public readonly DbType Type;

    public CommandParameter(string name, T? value, DbType? type = null)
    {
        Name = name;
        Value = value;
        Type = type ?? value.GetDbType();
    }
}