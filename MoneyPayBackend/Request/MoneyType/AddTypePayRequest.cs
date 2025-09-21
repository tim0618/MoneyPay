namespace MoneyPayBackend.Request
{
    public class AddTypePayRequest
    {
        public int moneyTypeId { get; set; }
        public int price { get; set; }
        public DateTime createTime { get; set; }
    }
}