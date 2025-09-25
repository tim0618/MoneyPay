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

    public bool AddDefaultTypesForUser(string email)
    {
        try
        {
            _context.Database.ExecuteSqlRaw("EXEC dbo.AddDefaultTypesForUser @email={0}", email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool AddDefaultTypeRemarksForUser(string email)
    {
        try
        {
            _context.Database.ExecuteSqlRaw("EXEC dbo.AddDefaultTypeRemarksForUser @email={0}", email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public List<MoneyTypesRequest> GetMoneyTypesSum(string email)
    {
        return _context.MoneyTypes
             .Where(mt => mt.email == email)
             .Select(mt => new MoneyTypesRequest
             {
                 moneyTypeId = mt.moneyTypeId,
                 email = mt.email,
                 type = mt.type,
                 icon = mt.icon,
                 color = mt.color,
                 categoryType = mt.categoryType,
                 totalPay = _context.MoneyTypeDetail
                     .Where(d => d.remarkId != null &&
                                 _context.MoneyTypeDetailRemark
                                     .Where(r => r.moneyTypeId == mt.moneyTypeId)
                                     .Select(r => r.remarkId)
                                     .Contains(d.remarkId))
                     .Sum(d => (int?)d.price) ?? 0
             })
             .ToList();
    }

    public List<MoneyTypeDetailRemarkModel> GetTypeRemarkById(int typeId)
    {
        return _context.MoneyTypeDetailRemark.Where(mtdr => mtdr.moneyTypeId == typeId).ToList();
    }

    public bool AddTypePay(MoneyTypeDetailModel typeDetail)
    {
        _context.MoneyTypeDetail.Add(typeDetail);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateTypePay(MoneyTypeDetailModel updateTypeDetail)
    {
        var typeDetail = _context.MoneyTypeDetail.FirstOrDefault(md => md.moneyTypeDetailId == updateTypeDetail.moneyTypeDetailId);
        if (typeDetail == null) return false;

        typeDetail.price = updateTypeDetail.price;
        typeDetail.remarkId = updateTypeDetail.remarkId;
        typeDetail.createTime = updateTypeDetail.createTime;

        return _context.SaveChanges() > 0;
    }

    public bool DeleteTypePay(int typeDetailId)
    {
        var typeDetail = _context.MoneyTypeDetail.FirstOrDefault(md => md.moneyTypeDetailId == typeDetailId);
        if (typeDetail == null) return false;

        _context.MoneyTypeDetail.Remove(typeDetail);
        return _context.SaveChanges() > 0;
    }

}