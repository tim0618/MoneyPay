using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Model
{
    public class MoneyTypesModel
    {
        [Key]
        public int Id { get; set; }
        public string? email { get; set; }
        public string? type { get; set; }
        public string? icon { get; set; }
        public string? color { get; set; }
        public string? categoryType { get; set; }
        public int totalPay { get; set; }
    }
}