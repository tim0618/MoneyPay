using System.Data.Common;
using MoneyPayBackend.IRepo;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.Repo
{
    public class UserRecordRepo : IUserRecordRepo
    {
        private readonly MoneyPayDBContext _context;
        public UserRecordRepo(MoneyPayDBContext context)
        {
            _context = context;
        }
        public (IEnumerable<DateTime> recordDays, int recordCount) GetBookRecords(string userEmail)
        {
            var userId = _context.Users
                .Where(u => u.email == userEmail)
                .Select(u => (int?)u.userId)
                .FirstOrDefault();

            if (userId == null)
            {
                return (Enumerable.Empty<DateTime>(), 0);
            }

            var resolvedUserId = userId.Value;

            var recordDays = _context.Categories
                .Where(mt => mt.userId == resolvedUserId)
                .SelectMany(mt => _context.Transactions
                    .Where(d => _context.CategoryItems
                        .Where(dr => dr.moneyTypeId == mt.moneyTypeId)
                        .Select(dr => dr.remarkId)
                        .Contains(d.remarkId))
                    .Select(d => d.createTime.Date)
                )
                .Distinct();

            var recordCount = _context.Categories
                 .Where(mt => mt.userId == resolvedUserId)
                 .Sum(mt => _context.Transactions
                        .Count(d => _context.CategoryItems
                            .Where(r => r.moneyTypeId == mt.moneyTypeId)
                            .Select(r => r.remarkId)
                            .Contains(d.remarkId))
                  );

            return (recordDays, recordCount);
        }
    }

}
