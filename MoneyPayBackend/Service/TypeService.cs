using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Model;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.Service
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepo _typeRepo;

        public TypeService(ITypeRepo typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public List<MoneyTypesModel> GetMoneyTypesSum(string userEmail)
        {
            return _typeRepo.GetMoneyTypesSum(userEmail);
        }
        public bool AddTypePay(AddTypePayRequest addTypePay)
        {
            var typeDetail = new MoneyTypeDetailModel
            {
                moneyTypeId = addTypePay.moneyTypeId,
                price = addTypePay.price,
                createTime = addTypePay.createTime
            };
            return _typeRepo.AddTypePay(typeDetail);
        }
        public bool UpdateTypePay(UpdateTypePayRequest updateTypePay)
        {
            var updateTypeDetail = new MoneyTypeDetailModel
            {
                Id = updateTypePay.Id,
                price = updateTypePay.price,
                createTime = DateTime.Now
            };
            return _typeRepo.UpdateTypePay(updateTypeDetail);
        }
        public bool DeleteTypePay(DeleteTypePayRequest deleteTypePay)
        {
            return _typeRepo.DeleteTypePay(deleteTypePay.Id);
        }
    }
}