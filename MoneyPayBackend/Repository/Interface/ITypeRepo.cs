using MoneyPayBackend.Model;

namespace MoneyPayBackend.IRepo
{
    public interface ITypeRepo
    {
        bool AddDefaultTypesForUser(string email);
        List<MoneyTypesModel> GetTypesByEmail(string email);
    }
}