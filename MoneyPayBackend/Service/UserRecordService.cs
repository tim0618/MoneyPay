using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.Service
{
    public class UserRecordService : IUserRecordService
    {
        private readonly IUserRecordRepo _userRecordRepo;
        public UserRecordService(IUserRecordRepo userRecordRepo)
        {
            _userRecordRepo = userRecordRepo;
        }

        public GetBookRecordsResponse GetBookRecords(string userEmail)
        {
            var recordData = _userRecordRepo.GetBookRecords(userEmail);

            var recordDaysCount = recordData.recordDays.Count();

            var today = DateTime.Today;
            int recordContinuous = 0;
            if (recordData.recordDays.Contains(today)) recordContinuous++;
            while (recordData.recordDays.Contains(today.AddDays(-1)))
            {
                recordContinuous++;
                today = today.AddDays(-1);
            }

            return new GetBookRecordsResponse
            {
                recordCount = recordData.recordCount,
                recordDaysCount = recordDaysCount,
                recordContinuous = recordContinuous
            };
        }
    }
}