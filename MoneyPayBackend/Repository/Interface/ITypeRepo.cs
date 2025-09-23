using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.IRepo
{
    public interface ITypeRepo
    {
        bool AddDefaultTypesForUser(string email);
        bool AddDefaultTypeRemarksForUser(string email);
        List<MoneyTypesRequest> GetMoneyTypesSum(string email);
        List<MoneyTypeDetailRemarkModel> GetTypeRemarkById(int typeId);
        bool AddTypePay(MoneyTypeDetailModel typeDetail);
        bool UpdateTypePay(MoneyTypeDetailModel updateTypeDetail);
        bool DeleteTypePay(int typeDetailId);
    }
}