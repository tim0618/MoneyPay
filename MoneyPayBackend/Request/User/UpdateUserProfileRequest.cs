using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Request
{
    public class UpdateUserProfileRequest
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; } = string.Empty;
    }
}
