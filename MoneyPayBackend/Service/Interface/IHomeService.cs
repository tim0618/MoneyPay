using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.IService
{
    public interface IHomeService
    {
        GetHomeSummaryResponse GetSummary(string userEmail, GetHomeSummaryRequest getHomeSummaryRequest);
    }
}
