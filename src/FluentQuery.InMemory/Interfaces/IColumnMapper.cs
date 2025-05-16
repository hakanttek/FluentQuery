using System.Data.Common;

namespace FluentQuery.InMemory.Interfaces;

public interface IColumnMapper
{
    public T Map<T>(DbDataReader reader);
}
