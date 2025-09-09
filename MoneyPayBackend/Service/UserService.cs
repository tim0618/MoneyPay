using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.Service;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;
    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public UserResponse GetUserById(Guid id)
    {
        var userData = _userRepo.GetUserById(id);

        var a = new UserResponse
        {
            Id = userData.Id,
            name = userData.name,
            email = userData.email
        };
        return a;
    }
}