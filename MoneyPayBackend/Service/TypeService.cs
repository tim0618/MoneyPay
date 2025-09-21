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

        public List<MoneyTypesModel> GetMoneyTypesSum(string userEmail)
        {
            return _typeRepo.GetMoneyTypesSum(userEmail);
        }
        public bool AddTypePay(TypePay typePay)
        {
            var moneyTypeId = typePay.moneyTypeId;
            var price = typePay.price;
            var createTime = typePay.createTime;
            var typeDetail = new MoneyTypeDetailModel
            {
                moneyTypeId = moneyTypeId,
                price = price,
                createTime = createTime
            };
            return _typeRepo.AddTypePay(typeDetail);
        }
    }
}