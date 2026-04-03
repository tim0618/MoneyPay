using MoneyPayBackend.Model;
using MoneyPayBackend.Request;
using MoneyPayBackend.Response;

namespace MoneyPayBackend.IService
{
    public interface ITypeService
    {
        List<GetTypePayResponse> GetMoneyTypesSum(string userEmail);
        List<GetMoneyTypeCategoryResponse> GetMoneyTypes(string userEmail, string? categoryType);
        List<GetMoneyTypeCategoryResponse> GetDeletedMoneyTypes(string userEmail, string? categoryType);
        GetMoneyTypeCategoryResponse CreateMoneyType(string userEmail, CreateMoneyTypeRequest createMoneyTypeRequest);
        GetMoneyTypeCategoryResponse CreateSubMoneyType(string userEmail, int parentId, CreateSubMoneyTypeRequest createSubMoneyTypeRequest);
        GetMoneyTypeCategoryResponse UpdateSubMoneyType(string userEmail, int subCategoryId, UpdateSubMoneyTypeRequest updateSubMoneyTypeRequest);
        bool DeleteMoneyType(string userEmail, int typeId);
        bool DeleteSubMoneyType(string userEmail, int subCategoryId);
        bool RestoreMoneyType(string userEmail, int typeId);
        bool RestoreSubMoneyType(string userEmail, int subCategoryId);
        List<GetTypeRemarkResponse> GetTypeRemarkById(int typeId);
        bool AddTypePay(AddTypePayRequest addTypePay);
        bool UpdateTypePay(UpdateTypePayRequest updateTypePay);
        bool DeleteTypePay(DeleteTypePayRequest deleteTypePay);
    }
}
