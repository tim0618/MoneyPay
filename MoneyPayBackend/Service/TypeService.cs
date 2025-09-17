using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MineyPayBackend.Service
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepo _typeRepo;

        public TypeService(ITypeRepo typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public List<MoneyTypesModel> GetMoneyTypesByEmail(string userEmail)
        {
            var allType = _typeRepo.GetMoneyTypesByEmail(userEmail);
            return allType;
        }

    }
}