using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyPayBackend.Model
{
    [Table("CategoryItems")]
    public class MoneyTypeDetailRemarkModel
    {
        [Key]
        [Column("categoryItemId")]
        public int remarkId { get; set; }

        [Column("categoryId")]
        public int moneyTypeId { get; set; }

        [Column("name")]
        public string remark { get; set; }

        [Column("isDeleted")]
        public bool isDeleted { get; set; }

        [Column("deletedAt")]
        public DateTime? deletedAt { get; set; }
    }
}
