using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyPayBackend.Model
{
    [Table("Transactions")]
    public class MoneyTypeDetailModel
    {
        [Key]
        [Column("transactionId")]
        public int moneyTypeDetailId { get; set; }

        [Column("price")]
        public int price { get; set; }

        [Column("categoryItemId")]
        public int remarkId { get; set; }

        [Column("createTime")]
        public DateTime createTime { get; set; }
    }
}
