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
            var recordDays = _context.MoneyTypes
                .Where(mt => mt.email == userEmail)
                .SelectMany(mt => _context.MoneyTypeDetail
                    .Where(d => _context.MoneyTypeDetailRemark
                        .Where(dr => dr.moneyTypeId == mt.moneyTypeId)
                        .Select(dr => dr.remarkId)
                        .Contains(d.remarkId))
                    .Select(d => d.createTime.Date)
                )
                .Distinct();

            var recordCount = _context.MoneyTypes
                 .Where(mt => mt.email == userEmail)
                 .Sum(mt => _context.MoneyTypeDetail
                        .Count(d => _context.MoneyTypeDetailRemark
                            .Where(r => r.moneyTypeId == mt.moneyTypeId)
                            .Select(r => r.remarkId)
                            .Contains(d.remarkId))
                  );

            return (recordDays, recordCount);
        }
    }

}