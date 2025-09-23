using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Model
{
    public class MoneyTypeDetailModel
    {
        [Key]
        public int moneyTypeId { get; set; }
        public int price { get; set; }
        public int remarkId { get; set; }
        public DateTime createTime { get; set; }
    }
}