
namespace MoneyPayBackend.IRepo
{
    public interface IUserRecordRepo
    {
        public (IEnumerable<DateTime> recordDays, int recordCount) GetBookRecords(string userEmail);
    }
}