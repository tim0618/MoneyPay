using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyPayBackend.Model
{
    [Table("Categories")]
    public class MoneyTypesModel
    {
        [Key]
        [Column("categoryId")]
        public int moneyTypeId { get; set; }

        [Column("userId")]
        public int userId { get; set; }

        [Column("typeName")]
        public string? typeName { get; set; }

        [Column("icon")]
        public string? icon { get; set; }

        [Column("color")]
        public string? color { get; set; }

        [Column("categoryType")]
        public string? categoryType { get; set; }

        [Column("isDeleted")]
        public bool isDeleted { get; set; }

        [Column("deletedAt")]
        public DateTime? deletedAt { get; set; }
    }
}
