using MoneyPayBackend.Model;

namespace MoneyPayBackend.IRepo
{
    public interface ITypeRepo
    {
        bool AddDefaultTypesForUser(string email);
        List<MoneyTypesModel> GetMoneyTypesSum(string email);
        bool AddTypePay(MoneyTypeDetailModel typeDetail);
        bool UpdateTypePay(MoneyTypeDetailModel updateTypeDetail);
    }
}