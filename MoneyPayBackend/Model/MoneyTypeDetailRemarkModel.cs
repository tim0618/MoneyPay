using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Model
{
    public class MoneyTypeDetailRemarkModel
    {
        [Key]
        public int remarkId { get; set; }
        public int moneyTypeId { get; set; }
        public string remark { get; set; }
    }
}