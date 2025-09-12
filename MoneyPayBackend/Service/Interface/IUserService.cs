using MoneyPayBackend.Model;
using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.IService;

public interface IUserService
{
    LoginResponse Login(LoginRequest loginRequest);
}