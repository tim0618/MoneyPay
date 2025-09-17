using MoneyPayBackend.IRepo;
using MoneyPayBackend.Model;

namespace MoneyPayBackend.Repo;

public class TypeRepo : ITypeRepo
{
    private readonly MoneyPayDBContext _context;

    public TypeRepo(MoneyPayDBContext context)
    {
        _context = context;
    }

    public bool AddDefaultTypesForUser(string email)
    {
        var defaultTypes = new List<MoneyTypesModel>
        {
            new MoneyTypesModel { email = email, type = "Food", icon = "mdi-silverware-fork-knife",color="#FF7043" },
            new MoneyTypesModel { email = email, type = "Transport", icon = "mdi-car" ,color="#29B6F6" },
            new MoneyTypesModel { email = email, type = "Entertainment", icon = "mdi-gamepad-variant" ,color="#AB47BC" },
            new MoneyTypesModel { email = email, type = "Shopping", icon = "mdi-cart" ,color="#66BB6A" },
            new MoneyTypesModel { email = email, type = "Healthcare", icon = "mdi-hospital-box" ,color="#EF5350" },
            new MoneyTypesModel { email = email, type = "Education", icon = "mdi-school" ,color="#42A5F5" },
            new MoneyTypesModel { email = email, type = "Family", icon = "mdi-home" ,color="#8D6E63" },
            new MoneyTypesModel { email = email, type = "Travel", icon = "mdi-airplane" ,color="#FFA726" },
            new MoneyTypesModel { email = email, type = "Other", icon = "mdi-dots-horizontal" ,color="#9E9E9E" },
        };

        _context.MoneyTypes.AddRange(defaultTypes);
        return _context.SaveChanges() > 0;
    }

    public List<MoneyTypesModel> GetTypesByEmail(string email)
    {
        var allTypes = _context.MoneyTypes.Where(t => t.email == email).ToList();
        return allTypes;
    }
}