using MoneyPayBackend.Model;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.IService;
public interface IUserService
{
    UserResponse GetUserById(Guid id);
}