using TransactionApi.Models.Expections;
using TransactionApi.Data.Converter.Contract;

namespace TransactionApi.Data.Converter.Implementations
{
    public class UidConverter : IParser<Guid, string>,  IParser<string, Guid>, IParser<object, Guid>
    {
        public string Parse(Guid origin)
        {
            return IsValidGuid(origin.ToString())
                ? origin.ToString() : throw new InvalidUidFormatExpection();
        }

        public Guid Parse(string origin)
        {
            try
            {
                return Guid.ParseExact(origin, "D");
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Cannot convert uid.");
            }
            catch (ArgumentNullException)
            {   
                throw new ArgumentException("Uid cannot be null.");
            }
            catch (FormatException)
            {   
                throw new InvalidUidFormatExpection();
            }
        }

        public Guid Parse(object origin)
        {
            return Parse((string)origin);
        }

        private bool IsValidGuid(string origin)
        {
            return Parse(origin) != null ? true : false;
        }
    }
}