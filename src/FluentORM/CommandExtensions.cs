using System.Data;
using System.Data.Common;

namespace FluentORM;

public static class CommandExtensions
{
    public static int AddParameter<T>(this DbCommand command, string name, T? value, DbType? type = null)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value is null ? DBNull.Value : value;
        parameter.DbType = type ?? value.GetDbType();
        return command.Parameters.Add(parameter);
    }

    public static int AddParameter(this DbCommand command, CommandParameter parameter)
        => AddParameter(command, parameter.Name, parameter.Value, parameter.Type);

    public static void AddParameter(this DbCommand command, params CommandParameter[] parameters)
    {
        foreach (var parameter in parameters)
            command.AddParameter(parameter);
    }

    public static DbType GetDbType<T>(this T? obj)
    {
        Type type = obj?.GetType() ?? typeof(T);

        return TypeMap.TryGetValue(type, out var dbType)
            ? dbType
            : throw new NotSupportedException($"Type '{type.FullName}' is not supported.");
    }

    private static readonly Dictionary<Type, DbType> TypeMap = new Dictionary<Type, DbType>(37)
    {
        [typeof(byte)] = DbType.Byte,
        [typeof(sbyte)] = DbType.SByte,
        [typeof(short)] = DbType.Int16,
        [typeof(ushort)] = DbType.UInt16,
        [typeof(int)] = DbType.Int32,
        [typeof(uint)] = DbType.UInt32,
        [typeof(long)] = DbType.Int64,
        [typeof(ulong)] = DbType.UInt64,
        [typeof(float)] = DbType.Single,
        [typeof(double)] = DbType.Double,
        [typeof(decimal)] = DbType.Decimal,
        [typeof(bool)] = DbType.Boolean,
        [typeof(string)] = DbType.String,
        [typeof(char)] = DbType.StringFixedLength,
        [typeof(Guid)] = DbType.Guid,
        [typeof(DateTime)] = DbType.DateTime,
        [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
        [typeof(TimeSpan)] = DbType.Time,
        [typeof(byte[])] = DbType.Binary,
        [typeof(byte?)] = DbType.Byte,
        [typeof(sbyte?)] = DbType.SByte,
        [typeof(short?)] = DbType.Int16,
        [typeof(ushort?)] = DbType.UInt16,
        [typeof(int?)] = DbType.Int32,
        [typeof(uint?)] = DbType.UInt32,
        [typeof(long?)] = DbType.Int64,
        [typeof(ulong?)] = DbType.UInt64,
        [typeof(float?)] = DbType.Single,
        [typeof(double?)] = DbType.Double,
        [typeof(decimal?)] = DbType.Decimal,
        [typeof(bool?)] = DbType.Boolean,
        [typeof(char?)] = DbType.StringFixedLength,
        [typeof(Guid?)] = DbType.Guid,
        [typeof(DateTime?)] = DbType.DateTime,
        [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
        [typeof(TimeSpan?)] = DbType.Time,
        [typeof(object)] = DbType.Object
    };

    public static CommandParameter ToParam<T>(this T? obj, string name, DbType? type = null)
    {
        return CommandParameter.Create(name, obj, type);
    }
}