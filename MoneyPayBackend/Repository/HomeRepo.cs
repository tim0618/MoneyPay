using MoneyPayBackend.IRepo;
using MoneyPayBackend.Model;

namespace MoneyPayBackend.Repo
{
    public class HomeRepo : IHomeRepo
    {
        private readonly MoneyPayDBContext _context;

        public HomeRepo(MoneyPayDBContext context)
        {
            _context = context;
        }

        public List<HomeSummaryCategoryModel> GetMonthlyCategoryAmounts(string userEmail, int year, int month)
        {
            var userId = ResolveUserId(userEmail);
            if (userId == null)
            {
                return new List<HomeSummaryCategoryModel>();
            }

            var monthStart = new DateTime(year, month, 1);
            var nextMonthStart = monthStart.AddMonths(1);
            var resolvedUserId = userId.Value;

            return _context.Categories
                .Where(mt => mt.userId == resolvedUserId)
                .OrderBy(mt => mt.moneyTypeId)
                .Select(mt => new HomeSummaryCategoryModel
                {
                    id = mt.moneyTypeId,
                    name = mt.typeName ?? string.Empty,
                    icon = mt.icon,
                    color = mt.color,
                    categoryType = mt.categoryType,
                    isDeleted = mt.isDeleted,
                    amount = (
                        from remark in _context.CategoryItems
                        join detail in _context.Transactions on remark.remarkId equals detail.remarkId
                        where remark.moneyTypeId == mt.moneyTypeId
                              && detail.createTime >= monthStart
                              && detail.createTime < nextMonthStart
                        select (int?)detail.price
                    ).Sum() ?? 0
                })
                .ToList();
        }

        private int? ResolveUserId(string email)
        {
            return _context.Users
                .Where(u => u.email == email)
                .Select(u => (int?)u.userId)
                .FirstOrDefault();
        }
    }
}
