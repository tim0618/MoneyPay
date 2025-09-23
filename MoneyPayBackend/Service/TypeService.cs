using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Model;
using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.Service
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepo _typeRepo;

        public TypeService(ITypeRepo typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public List<GetTypePayResponse> GetMoneyTypesSum(string userEmail)
        {
            var types = _typeRepo.GetMoneyTypesSum(userEmail);

            var result = types.Select(t => new GetTypePayResponse
            {
                Id = t.moneyTypeId,
                type = t.type,
                icon = t.icon,
                color = t.color,
                categoryType = t.categoryType,
                totalPay = t.totalPay
            }).ToList();
            return result;
        }
        public List<GetTypeRemarkResponse> GetTypeRemarkById(int typeId)
        {
            var typeRemarks = _typeRepo.GetTypeRemarkById(typeId);
            var result = typeRemarks.Select(tr => new GetTypeRemarkResponse
            {
                remarkId = tr.remarkId,
                remark = tr.remark,
            }).ToList();
            return result;
        }
        public bool AddTypePay(AddTypePayRequest addTypePay)
        {
            var typeDetail = new MoneyTypeDetailModel
            {
                price = addTypePay.price,
                remarkId = addTypePay.remarkId,
                createTime = DateTime.Now
            };
            return _typeRepo.AddTypePay(typeDetail);
        }
        public bool UpdateTypePay(UpdateTypePayRequest updateTypePay)
        {
            var updateTypeDetail = new MoneyTypeDetailModel
            {
                moneyTypeId = updateTypePay.moneyTypeId,
                price = updateTypePay.price,
                remarkId = updateTypePay.remarkId,
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