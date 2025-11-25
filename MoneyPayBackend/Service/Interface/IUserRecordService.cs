using MoneyPayBackend.Response;

namespace MoneyPayBackend.IService
{
    public interface IUserRecordService
    {
        GetBookRecordsResponse GetBookRecords(string userEmail);
    }
}