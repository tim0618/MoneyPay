using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Request
{
    public class GetHomeSummaryRequest
    {
        [Range(2000, 9999)]
        public int year { get; set; }

        [Range(1, 12)]
        public int month { get; set; }
    }
}
