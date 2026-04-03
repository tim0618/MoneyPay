namespace MoneyPayBackend.Response
{
    public class GetMoneyTypeCategoryResponse
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? icon { get; set; }
        public string? color { get; set; }
        public string categoryType { get; set; } = string.Empty;
        public bool isDeleted { get; set; }
        public List<GetMoneyTypeCategoryResponse> children { get; set; } = new();
    }
}
