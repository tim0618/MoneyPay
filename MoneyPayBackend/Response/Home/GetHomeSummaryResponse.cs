namespace MoneyPayBackend.Response
{
    public class GetHomeSummaryResponse
    {
        public int year { get; set; }
        public int month { get; set; }
        public int expenseTotal { get; set; }
        public int incomeTotal { get; set; }
        public List<GetHomeSummaryCategoryResponse> expenseCategories { get; set; } = new();
        public List<GetHomeSummaryCategoryResponse> incomeCategories { get; set; } = new();
    }
}
