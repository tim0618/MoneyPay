namespace MoneyPayBackend.Response
{
    public class GetTypePayResponse
    {
        public int Id { get; set; }
        public string? type { get; set; }
        public string? icon { get; set; }
        public string? color { get; set; }
        public string? categoryType { get; set; }
        public int totalPay { get; set; }
    }
}