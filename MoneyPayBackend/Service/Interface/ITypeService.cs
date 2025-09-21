using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.IService
{
    public interface ITypeService
    {
        List<MoneyTypesModel> GetMoneyTypesSum(string userEmail);
    }
}