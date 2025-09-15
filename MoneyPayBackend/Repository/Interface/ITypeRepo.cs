using MoneyPayBackend.Model;

namespace MoneyPayBackend.IRepo
{
    public interface ITypeRepo
    {
        bool AddDefaultTypesForUser(string email);
        List<TypeModel> GetTypesByEmail(string email);
    }
}