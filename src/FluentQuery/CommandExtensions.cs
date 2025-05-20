using System.Data;
using System.Data.Common;

namespace FluentQuery;

public static class CommandExtensions
{
    public static int AddParameter<T>(this DbCommand command, string name, T? value, DbType? type = null)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value is null ? DBNull.Value : value;
        parameter.DbType = value.GetUnderlyingType().GetDbType();
        return command.Parameters.Add(parameter);
    }

    public static Type GetUnderlyingType<T>(this T? value)
    {
        return value?.GetType()
            ?? Nullable.GetUnderlyingType(typeof(T))
            ?? throw new InvalidOperationException("Cannot determine the underlying type for the given value.");
    }

    public static DbType GetDbType(this Type type) => type switch
    {
        var t when t == typeof(int) => DbType.Int32,
        var t when t == typeof(string) => DbType.String,
        var t when t == typeof(DateTime) => DbType.DateTime,
        var t when t == typeof(bool) => DbType.Boolean,
        var t when t == typeof(decimal) => DbType.Decimal,
        var t when t == typeof(Guid) => DbType.Guid,
        var t when t == typeof(long) => DbType.Int64,
        var t when t == typeof(double) => DbType.Double,
        _ => throw new NotSupportedException($"Type {type} is not supported.")
    };
}