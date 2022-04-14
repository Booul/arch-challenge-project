namespace BalanceApi.Models.Exceptions
{
    public class NoTransactionsException: Exception
    {
        public new string Message = "There are no transactions for this account number.";
    }
}