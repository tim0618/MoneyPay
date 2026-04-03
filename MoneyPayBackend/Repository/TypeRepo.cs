using Microsoft.EntityFrameworkCore;
using MoneyPayBackend.IRepo;
using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.Repo;

public class TypeRepo : ITypeRepo
{
    private readonly MoneyPayDBContext _context;

    public TypeRepo(MoneyPayDBContext context)
    {
        _context = context;
    }

    public bool AddDefaultTypesForUser(int userId)
    {
        try
        {
            _context.Database.ExecuteSqlRaw("EXEC dbo.AddDefaultTypesForUser @userId={0}", userId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool AddDefaultTypeRemarksForUser(int userId)
    {
        try
        {
            _context.Database.ExecuteSqlRaw("EXEC dbo.AddDefaultTypeRemarksForUser @userId={0}", userId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public List<MoneyTypesRequest> GetMoneyTypesSum(string email)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return new List<MoneyTypesRequest>();
        }

        var resolvedUserId = userId.Value;

        return _context.Categories
             .Where(mt => mt.userId == resolvedUserId && !mt.isDeleted)
             .OrderBy(mt => mt.moneyTypeId)
             .Select(mt => new MoneyTypesRequest
             {
                 moneyTypeId = mt.moneyTypeId,
                 typeName = mt.typeName,
                 icon = mt.icon,
                 color = mt.color,
                 categoryType = mt.categoryType,
                 totalPay = _context.Transactions
                     .Where(d => _context.CategoryItems
                                     .Where(r => r.moneyTypeId == mt.moneyTypeId)
                                     .Select(r => r.remarkId)
                                     .Contains(d.remarkId))
                     .Sum(d => (int?)d.price) ?? 0
             })
             .ToList();
    }

    public List<MoneyTypesModel> GetMoneyTypes(string email, string? categoryType = null)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return new List<MoneyTypesModel>();
        }

        var query = _context.Categories
            .Where(mt => mt.userId == userId.Value && !mt.isDeleted);

        if (!string.IsNullOrWhiteSpace(categoryType))
        {
            query = query.Where(mt => mt.categoryType == categoryType);
        }

        return query
            .OrderBy(mt => mt.moneyTypeId)
            .ToList();
    }

    public List<MoneyTypesModel> GetDeletedMoneyTypes(string email, string? categoryType = null)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return new List<MoneyTypesModel>();
        }

        var query = _context.Categories
            .Where(mt => mt.userId == userId.Value)
            .Where(mt => mt.isDeleted || _context.CategoryItems.Any(item => item.moneyTypeId == mt.moneyTypeId && item.isDeleted));

        if (!string.IsNullOrWhiteSpace(categoryType))
        {
            query = query.Where(mt => mt.categoryType == categoryType);
        }

        return query
            .OrderBy(mt => mt.moneyTypeId)
            .ToList();
    }

    public MoneyTypesModel? GetMoneyTypeById(string email, int typeId)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return null;
        }

        return _context.Categories
            .FirstOrDefault(mt => mt.userId == userId.Value && mt.moneyTypeId == typeId && !mt.isDeleted);
    }

    public MoneyTypesModel AddMoneyType(string email, MoneyTypesModel moneyType)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            throw new InvalidOperationException("User not found");
        }

        moneyType.userId = userId.Value;
        moneyType.isDeleted = false;
        moneyType.deletedAt = null;
        _context.Categories.Add(moneyType);
        _context.SaveChanges();
        return moneyType;
    }

    public MoneyTypeDetailRemarkModel AddTypeRemark(MoneyTypeDetailRemarkModel typeRemark)
    {
        typeRemark.isDeleted = false;
        typeRemark.deletedAt = null;
        _context.CategoryItems.Add(typeRemark);
        _context.SaveChanges();
        return typeRemark;
    }

    public MoneyTypeDetailRemarkModel? GetTypeRemarkDetailById(string email, int remarkId)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return null;
        }

        return (from remark in _context.CategoryItems
                join category in _context.Categories on remark.moneyTypeId equals category.moneyTypeId
                where remark.remarkId == remarkId
                      && !remark.isDeleted
                      && !category.isDeleted
                      && category.userId == userId.Value
                select remark)
            .FirstOrDefault();
    }

    public MoneyTypeDetailRemarkModel UpdateTypeRemark(MoneyTypeDetailRemarkModel typeRemark)
    {
        _context.CategoryItems.Update(typeRemark);
        _context.SaveChanges();
        return typeRemark;
    }

    public bool DeleteMoneyType(string email, int typeId)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return false;
        }

        var category = _context.Categories
            .FirstOrDefault(mt => mt.userId == userId.Value && mt.moneyTypeId == typeId);

        if (category == null)
        {
            return false;
        }

        category.isDeleted = true;
        category.deletedAt = DateTime.Now;

        var subCategories = _context.CategoryItems
            .Where(item => item.moneyTypeId == typeId && !item.isDeleted)
            .ToList();

        if (subCategories.Count > 0)
        {
            foreach (var subCategory in subCategories)
            {
                subCategory.isDeleted = true;
                subCategory.deletedAt = DateTime.Now;
            }
        }

        return _context.SaveChanges() > 0;
    }

    public bool DeleteSubMoneyType(string email, int remarkId)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return false;
        }

        var subCategory = (from remark in _context.CategoryItems
                           join category in _context.Categories on remark.moneyTypeId equals category.moneyTypeId
                           where remark.remarkId == remarkId
                                 && !remark.isDeleted
                                 && !category.isDeleted
                                 && category.userId == userId.Value
                           select remark)
            .FirstOrDefault();

        if (subCategory == null)
        {
            return false;
        }

        subCategory.isDeleted = true;
        subCategory.deletedAt = DateTime.Now;
        return _context.SaveChanges() > 0;
    }

    public List<MoneyTypeDetailRemarkModel> GetTypeRemarkById(int typeId)
    {
        return _context.CategoryItems
            .Where(mtdr => mtdr.moneyTypeId == typeId && !mtdr.isDeleted)
            .ToList();
    }

    public List<MoneyTypeDetailRemarkModel> GetDeletedTypeRemarkById(int typeId)
    {
        return _context.CategoryItems
            .Where(mtdr => mtdr.moneyTypeId == typeId && mtdr.isDeleted)
            .ToList();
    }

    public bool RestoreMoneyType(string email, int typeId)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return false;
        }

        var category = _context.Categories
            .FirstOrDefault(mt => mt.userId == userId.Value && mt.moneyTypeId == typeId && mt.isDeleted);

        if (category == null)
        {
            return false;
        }

        category.isDeleted = false;
        category.deletedAt = null;

        var subCategories = _context.CategoryItems
            .Where(item => item.moneyTypeId == typeId && item.isDeleted)
            .ToList();

        foreach (var subCategory in subCategories)
        {
            subCategory.isDeleted = false;
            subCategory.deletedAt = null;
        }

        return _context.SaveChanges() > 0;
    }

    public bool RestoreSubMoneyType(string email, int remarkId)
    {
        var userId = ResolveUserId(email);
        if (userId == null)
        {
            return false;
        }

        var result = (from remark in _context.CategoryItems
                      join category in _context.Categories on remark.moneyTypeId equals category.moneyTypeId
                      where remark.remarkId == remarkId
                            && remark.isDeleted
                            && !category.isDeleted
                            && category.userId == userId.Value
                      select remark)
            .FirstOrDefault();

        if (result == null)
        {
            return false;
        }

        result.isDeleted = false;
        result.deletedAt = null;
        return _context.SaveChanges() > 0;
    }

    public bool AddTypePay(MoneyTypeDetailModel typeDetail)
    {
        _context.Transactions.Add(typeDetail);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateTypePay(MoneyTypeDetailModel updateTypeDetail)
    {
        var typeDetail = _context.Transactions.FirstOrDefault(md => md.moneyTypeDetailId == updateTypeDetail.moneyTypeDetailId);
        if (typeDetail == null) return false;

        typeDetail.price = updateTypeDetail.price;
        typeDetail.remarkId = updateTypeDetail.remarkId;
        typeDetail.createTime = updateTypeDetail.createTime;

        return _context.SaveChanges() > 0;
    }

    public bool DeleteTypePay(int typeDetailId)
    {
        var typeDetail = _context.Transactions.FirstOrDefault(md => md.moneyTypeDetailId == typeDetailId);
        if (typeDetail == null) return false;

        _context.Transactions.Remove(typeDetail);
        return _context.SaveChanges() > 0;
    }

    private int? ResolveUserId(string email)
    {
        return _context.Users
            .Where(u => u.email == email)
            .Select(u => (int?)u.userId)
            .FirstOrDefault();
    }
}
