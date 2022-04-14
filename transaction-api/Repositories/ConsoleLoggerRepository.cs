using TransactionApi.Models.Enums;

namespace TransactionApi.Repositories
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