using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Model
{
    public class TypeModel
    {
        [Key]
        public int Id { get; set; }
        public string? email { get; set; }
        public string? type { get; set; }
        public string? icon { get; set; }
        public string? color { get; set; }
    }
}