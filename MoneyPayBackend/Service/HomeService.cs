using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Model;
using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.Service
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepo _homeRepo;

        public HomeService(IHomeRepo homeRepo)
        {
            _homeRepo = homeRepo;
        }

        public GetHomeSummaryResponse GetSummary(string userEmail, GetHomeSummaryRequest getHomeSummaryRequest)
        {
            var categoryAmounts = _homeRepo.GetMonthlyCategoryAmounts(
                userEmail,
                getHomeSummaryRequest.year,
                getHomeSummaryRequest.month);

            var expenseCategories = categoryAmounts
                .Where(category => category.categoryType == "Expense")
                .Where(ShouldDisplayCategory)
                .OrderBy(category => category.id)
                .Select(MapSummaryCategory)
                .ToList();

            var incomeCategories = categoryAmounts
                .Where(category => category.categoryType == "Income")
                .Where(ShouldDisplayCategory)
                .OrderBy(category => category.id)
                .Select(MapSummaryCategory)
                .ToList();

            return new GetHomeSummaryResponse
            {
                year = getHomeSummaryRequest.year,
                month = getHomeSummaryRequest.month,
                expenseCategories = expenseCategories,
                incomeCategories = incomeCategories,
                expenseTotal = expenseCategories.Sum(category => category.amount),
                incomeTotal = incomeCategories.Sum(category => category.amount)
            };
        }

        private static GetHomeSummaryCategoryResponse MapSummaryCategory(HomeSummaryCategoryModel category)
        {
            return new GetHomeSummaryCategoryResponse
            {
                id = category.id,
                name = category.name,
                icon = category.icon,
                color = category.color,
                amount = category.amount,
                isDeleted = category.isDeleted
            };
        }

        private static bool ShouldDisplayCategory(HomeSummaryCategoryModel category)
        {
            return !category.isDeleted || category.amount > 0;
        }
    }
}
