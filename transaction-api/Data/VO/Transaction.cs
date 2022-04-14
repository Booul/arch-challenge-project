namespace TransactionApi.Data.VO
{
    public class TransactionVO
    {
        public string UidAccount { get; set; } = String.Empty;
        public short TypeId { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"UidAccount: {UidAccount}, Type: {TypeId}, Value: {Value}, Date: {Date}";
        }
    }
}