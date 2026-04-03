using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.IRepo
{
    public interface ITypeRepo
    {
        bool AddDefaultTypesForUser(int userId);
        bool AddDefaultTypeRemarksForUser(int userId);
        List<MoneyTypesRequest> GetMoneyTypesSum(string email);
        List<MoneyTypesModel> GetMoneyTypes(string email, string? categoryType = null);
        List<MoneyTypesModel> GetDeletedMoneyTypes(string email, string? categoryType = null);
        MoneyTypesModel? GetMoneyTypeById(string email, int typeId);
        MoneyTypesModel AddMoneyType(string email, MoneyTypesModel moneyType);
        MoneyTypeDetailRemarkModel AddTypeRemark(MoneyTypeDetailRemarkModel typeRemark);
        MoneyTypeDetailRemarkModel? GetTypeRemarkDetailById(string email, int remarkId);
        MoneyTypeDetailRemarkModel UpdateTypeRemark(MoneyTypeDetailRemarkModel typeRemark);
        bool DeleteMoneyType(string email, int typeId);
        bool DeleteSubMoneyType(string email, int remarkId);
        List<MoneyTypeDetailRemarkModel> GetTypeRemarkById(int typeId);
        List<MoneyTypeDetailRemarkModel> GetDeletedTypeRemarkById(int typeId);
        bool RestoreMoneyType(string email, int typeId);
        bool RestoreSubMoneyType(string email, int remarkId);
        bool AddTypePay(MoneyTypeDetailModel typeDetail);
        bool UpdateTypePay(MoneyTypeDetailModel updateTypeDetail);
        bool DeleteTypePay(int typeDetailId);
    }
}
