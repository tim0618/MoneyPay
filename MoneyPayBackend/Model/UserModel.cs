using System.ComponentModel.DataAnnotations;

namespace MoneyPayBackend.Model
{
    public class UserModel
    {
        [Key]
        public string? email { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
        public DateTime createTime { get; set; }
    }
}