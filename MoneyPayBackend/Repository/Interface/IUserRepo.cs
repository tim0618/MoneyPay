using MoneyPayBackend.Model;

namespace MoneyPayBackend.IRepo;
public interface IUserRepo
{
    UserModel GetUserById(Guid id);
}