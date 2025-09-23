namespace MoneyPayBackend.Request
{
    public class MoneyTypesRequest
    {
        public int moneyTypeId { get; set; }
        public string? email { get; set; }
        public string? type { get; set; }
        public string? icon { get; set; }
        public string? color { get; set; }
        public string? categoryType { get; set; }
        public int totalPay { get; set; }
    }
}