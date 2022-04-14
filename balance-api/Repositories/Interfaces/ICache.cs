namespace BalanceApi.Repositories
{
    public interface ICache
    {
        string Get (string key);
        void Set (string key, string value);
        void Remove (string key);
    }
}