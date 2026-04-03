using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Request
{
    public class CreateMoneyTypeRequest
    {
        [Required]
        [StringLength(50)]
        public string typeName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string icon { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string color { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string categoryType { get; set; } = string.Empty;
    }
}
