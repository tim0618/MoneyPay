using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Model
{
    public class MoneyTypesModel
    {
        [Key]
        public int moneyTypeId { get; set; }
        public string? email { get; set; }
        public string? typeName { get; set; }
        public string? icon { get; set; }
        public string? color { get; set; }
        public string? categoryType { get; set; }
    }
}