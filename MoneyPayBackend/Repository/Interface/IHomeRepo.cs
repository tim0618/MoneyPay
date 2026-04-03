using MoneyPayBackend.Model;

namespace MoneyPayBackend.IRepo
{
    public interface IHomeRepo
    {
        List<HomeSummaryCategoryModel> GetMonthlyCategoryAmounts(string userEmail, int year, int month);
    }
}
