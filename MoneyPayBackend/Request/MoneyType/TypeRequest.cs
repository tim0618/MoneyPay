namespace MoneyPayBackend.Request
{
    public class TypePay
    {
        public int moneyTypeId { get; set; }
        public int price { get; set; }
        public DateTime createTime { get; set; }
    }
}