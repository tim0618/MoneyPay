namespace MoneyPayBackend.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime createTime { get; set; }
    }
}