using System.Data;

namespace FluentORM;

public record CommandParameter
{
    public readonly string Name;

    public readonly object? Value;

    public readonly DbType Type;

    private CommandParameter(string name, object? value, DbType type)
    {
        Name = name;
        Value = value;
        Type = type;
    }

    public static CommandParameter Create<T>(string name, T? value, DbType? type = null)
    {
        return new CommandParameter(name, value, type ?? value.GetDbType());
    }
}