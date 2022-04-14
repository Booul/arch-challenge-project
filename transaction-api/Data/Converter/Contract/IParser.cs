namespace TransactionApi.Data.Converter.Contract
{
    public interface IParser<O, D>
    {
        D? Parse (O origin);
    }
}