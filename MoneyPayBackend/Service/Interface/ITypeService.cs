using MoneyPayBackend.Model;
using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.IService
{
    public interface ITypeService
    {
        List<GetTypePayResponse> GetMoneyTypesSum(string userEmail);
        List<GetTypeRemarkResponse> GetTypeRemarkById(int typeId);
        bool AddTypePay(AddTypePayRequest addTypePay);
        bool UpdateTypePay(UpdateTypePayRequest updateTypePay);
        bool DeleteTypePay(DeleteTypePayRequest deleteTypePay);
    }
}