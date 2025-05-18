namespace FluentQuery.Interfaces;

public interface IConnectionBuilder<TDbConnection>
{
    public TDbConnection GetConnection();
}