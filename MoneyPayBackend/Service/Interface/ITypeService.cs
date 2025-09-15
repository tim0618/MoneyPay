using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.IService
{
    public interface ITypeService
    {
        List<TypeModel> GetTypesByEmail(TypeRequest typeRequest);
    }
}