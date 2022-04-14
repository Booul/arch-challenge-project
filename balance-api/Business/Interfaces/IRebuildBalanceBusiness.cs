namespace BalanceApi.Business.Interfaces
{
    public interface IRedisCacheBusiness
    {
        double GetBalance(string uidAccount);
        double? RebuildBalance(string uidAccount);
    }
}