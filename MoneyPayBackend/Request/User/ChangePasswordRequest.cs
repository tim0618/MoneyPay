using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string currentPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string newPassword { get; set; } = string.Empty;
    }
}
