namespace BalanceApi.Repositories
{
    public interface IDbRepository<T> where T: class
    {
        List<T>? List ();
        T? Post (T? item);
    }
}