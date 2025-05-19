using FluentQuery.Interfaces;
using System.Data.Common;

namespace FluentQuery;

// TODO: Create mapping profile to increasee performance
public class ColumnMapper : IColumnMapper
{
    public T Map<T>(DbDataReader reader)
    {
        var obj = Activator.CreateInstance<T>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            var prop = typeof(T).GetProperty(reader.GetName(i));
            if (prop != null && !reader.IsDBNull(i))
                prop.SetValue(obj, reader.GetValue(i));
        }
        return obj;
    }

}