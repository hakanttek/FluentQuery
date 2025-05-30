using System.Data.Common;

namespace FluentORM.Interfaces;

public interface IColumnMapper
{
    public T Map<T>(DbDataReader reader);
}