using BalanceApi.Models.Enums;

namespace BalanceApi.Repositories
{
    public class ConsoleLoggerRepository : ILogger
    {
        public void WriteLine(LoggerType type, string message)
        {
            string now = DateTime.Now.ToUniversalTime().ToString();

            Console.WriteLine($"{type.ToString()} - {now} - {message}");
        }
    }
}