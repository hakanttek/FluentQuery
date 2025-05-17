using System.Data.Common;

namespace FluentQuery.Interfaces;

public interface IColumnMapper
{
    public T Map<T>(DbDataReader reader);
}
