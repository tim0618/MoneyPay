using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Request
{
    public class CreateSubMoneyTypeRequest
    {
        [Required]
        [StringLength(50)]
        public string typeName { get; set; } = string.Empty;
    }
}
