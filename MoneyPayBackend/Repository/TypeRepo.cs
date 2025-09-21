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
            new MoneyTypesModel { email = email, type = "Food", icon = "mdi-silverware-fork-knife",color="#FF7043",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Transport", icon = "mdi-car" ,color="#29B6F6",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Entertainment", icon = "mdi-gamepad-variant" ,color="#AB47BC",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Shopping", icon = "mdi-cart" ,color="#66BB6A",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Healthcare", icon = "mdi-hospital-box" ,color="#EF5350",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Education", icon = "mdi-school" ,color="#42A5F5",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Family", icon = "mdi-home" ,color="#8D6E63",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Travel", icon = "mdi-airplane" ,color="#FFA726",categoryType="Expense" },
            new MoneyTypesModel { email = email, type = "Pet", icon = "mdi-paw" ,color="#9E9E9E",categoryType="Expense" },

            new MoneyTypesModel { email = email, type = "Salary", icon = "mdi-cash" ,color="#42A5F5",categoryType="Income" },
            new MoneyTypesModel { email = email, type = "Bonus", icon = "mdi-star-circle",color="#FF7043",categoryType="Income" },
            new MoneyTypesModel { email = email, type = "Other", icon = "mdi-dots-horizontal" ,color="#FFA726",categoryType="Income" },
        };

        _context.MoneyTypes.AddRange(defaultTypes);
        return _context.SaveChanges() > 0;
    }

    public List<MoneyTypesModel> GetMoneyTypesSum(string email)
    {
        return _context.MoneyTypes
             .Where(t => t.email == email)
             .GroupJoin(_context.MoneyTypeDetail,
             m => m.Id,
             md => md.moneyTypeId,
             (m, md) => new MoneyTypesModel
             {
                 Id = m.Id,
                 email = m.email,
                 type = m.type,
                 icon = m.icon,
                 color = m.color,
                 categoryType = m.categoryType,
                 totalPay = md.Sum(x => (int?)x.price) ?? 0
             }).ToList();
    }

    public bool AddTypePay(MoneyTypeDetailModel typeDetail)
    {
        _context.MoneyTypeDetail.Add(typeDetail);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateTypePay(MoneyTypeDetailModel updateTypeDetail)
    {
        var typeDetail = _context.MoneyTypeDetail.FirstOrDefault(md => md.Id == updateTypeDetail.Id);
        if (typeDetail == null) return false;

        typeDetail.price = updateTypeDetail.price;
        typeDetail.createTime = updateTypeDetail.createTime;

        return _context.SaveChanges() > 0;
    }

    public bool DeleteTypePay(int typeDetailId)
    {
        var typeDetail = _context.MoneyTypeDetail.FirstOrDefault(md => md.Id == typeDetailId);
        if (typeDetail == null) return false;

        _context.MoneyTypeDetail.Remove(typeDetail);
        return _context.SaveChanges() > 0;
    }

}