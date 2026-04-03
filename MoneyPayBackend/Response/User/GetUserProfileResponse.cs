namespace MoneyPayBackend.Response
{
    public class GetUserProfileResponse
    {
        public int userId { get; set; }
        public string email { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public DateTime createTime { get; set; }
    }
}
