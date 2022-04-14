using TransactionApi.Models.Interfaces;

namespace TransactionApi.Business.Interfaces
{
    public interface IBalanceBusiness<T, VO> where T: IBalance
    {
        T Save(VO data);
        void RebuildBalance();
    }
}