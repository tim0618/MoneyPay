namespace MoneyPayBackend.Model
{
    public class HomeSummaryCategoryModel
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? icon { get; set; }
        public string? color { get; set; }
        public string? categoryType { get; set; }
        public int amount { get; set; }
        public bool isDeleted { get; set; }
    }
}
