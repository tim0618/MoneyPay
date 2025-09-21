using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.IService
{
    public interface ITypeService
    {
        List<MoneyTypesModel> GetMoneyTypesSum(string userEmail);
        bool AddTypePay(AddTypePayRequest addTypePay);
        bool UpdateTypePay(UpdateTypePayRequest updateTypePay);
        bool DeleteTypePay(DeleteTypePayRequest deleteTypePay);
    }
}