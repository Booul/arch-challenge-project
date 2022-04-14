namespace TransactionApi.Models.Expections
{
    public class InvalidUidFormatExpection: Exception
    {
        public new string Message = "Invalid uid format.";
    }
}