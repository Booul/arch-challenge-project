using TransactionApi.Models.Enums;

namespace TransactionApi.Repositories
{
    public interface ILogger
    {
        void WriteLine (LoggerType type, string message);
    }
}