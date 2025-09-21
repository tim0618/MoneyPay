using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Model
{
    public class MoneyTypeDetail
    {
        [Key]
        public int Id { get; set; }
        public int moneyTypeId { get; set; }
        public int price { get; set; }
        public DateTime createTime { get; set; }
    }
}