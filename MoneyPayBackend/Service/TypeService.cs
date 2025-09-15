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

        public List<TypeModel> GetTypesByEmail(TypeRequest typeRequest)
        {
            var allType = _typeRepo.GetTypesByEmail(typeRequest.email);
            return allType;
        }

    }
}