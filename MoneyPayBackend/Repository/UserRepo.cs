using MoneyPayBackend.IRepo;
using MoneyPayBackend.Model;

namespace MoneyPayBackend.Repo;

public class UserRepo : IUserRepo
{
    private readonly MoneyPayDBContext _context;

    public UserRepo(MoneyPayDBContext context)
    {
        _context = context;
    }

    public UserModel GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.email == email)!;
    }

    public bool AddUser(UserModel user)
    {
        _context.Users.Add(user);
        return _context.SaveChanges() > 0;
    }

}