using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.IService
{
    public interface ITypeService
    {
        List<MoneyTypesModel> GetTypesByEmail(string userEmail);
    }
}