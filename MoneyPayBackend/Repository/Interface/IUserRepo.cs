using MoneyPayBackend.Model;

namespace MoneyPayBackend.IRepo;

public interface IUserRepo
{
    UserModel GetUserByEmail(string email);
    bool AddUser(UserModel user);
}