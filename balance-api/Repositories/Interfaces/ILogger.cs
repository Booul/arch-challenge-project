using BalanceApi.Models.Enums;

namespace BalanceApi.Repositories
{
    public interface ILogger
    {
        void WriteLine (LoggerType type, string message);
    }
}