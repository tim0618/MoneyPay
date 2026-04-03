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
                typeId = t.moneyTypeId,
                typeName = t.typeName,
                icon = t.icon,
                color = t.color,
                categoryType = t.categoryType,
                totalPay = t.totalPay
            }).ToList();
            return result;
        }

        public List<GetMoneyTypeCategoryResponse> GetMoneyTypes(string userEmail, string? categoryType)
        {
            var normalizedCategoryType = NormalizeCategoryType(categoryType);
            if (!string.IsNullOrWhiteSpace(categoryType) && normalizedCategoryType == null)
            {
                throw new ArgumentException("Invalid category type");
            }

            var moneyTypes = _typeRepo.GetMoneyTypes(userEmail, normalizedCategoryType);
            return moneyTypes
                .OrderBy(mt => mt.moneyTypeId)
                .Select(MapMoneyTypeWithChildren)
                .ToList();
        }

        public List<GetMoneyTypeCategoryResponse> GetDeletedMoneyTypes(string userEmail, string? categoryType)
        {
            var normalizedCategoryType = NormalizeCategoryType(categoryType);
            if (!string.IsNullOrWhiteSpace(categoryType) && normalizedCategoryType == null)
            {
                throw new ArgumentException("Invalid category type");
            }

            var moneyTypes = _typeRepo.GetDeletedMoneyTypes(userEmail, normalizedCategoryType);
            return moneyTypes
                .OrderBy(mt => mt.moneyTypeId)
                .Select(MapDeletedMoneyTypeWithChildren)
                .Where(category => category.isDeleted || category.children.Count > 0)
                .ToList();
        }

        public GetMoneyTypeCategoryResponse CreateMoneyType(string userEmail, CreateMoneyTypeRequest createMoneyTypeRequest)
        {
            var normalizedCategoryType = NormalizeCategoryType(createMoneyTypeRequest.categoryType);
            if (normalizedCategoryType == null)
            {
                throw new ArgumentException("Invalid category type");
            }

            var typeName = RequireValue(createMoneyTypeRequest.typeName, "Type name is required");
            var icon = RequireValue(createMoneyTypeRequest.icon, "Icon is required");
            var color = RequireValue(createMoneyTypeRequest.color, "Color is required");

            var createdType = _typeRepo.AddMoneyType(userEmail, new MoneyTypesModel
            {
                typeName = typeName,
                icon = icon,
                color = color,
                categoryType = normalizedCategoryType
            });

            return MapMoneyTypeWithChildren(createdType);
        }

        public GetMoneyTypeCategoryResponse CreateSubMoneyType(string userEmail, int parentId, CreateSubMoneyTypeRequest createSubMoneyTypeRequest)
        {
            var parentType = _typeRepo.GetMoneyTypeById(userEmail, parentId);
            if (parentType == null)
            {
                throw new KeyNotFoundException("Parent category not found");
            }

            var categoryType = parentType.categoryType;
            if (string.IsNullOrWhiteSpace(categoryType))
            {
                throw new ArgumentException("Parent category type is invalid");
            }

            var typeName = RequireValue(createSubMoneyTypeRequest.typeName, "Type name is required");

            var createdSubType = _typeRepo.AddTypeRemark(new MoneyTypeDetailRemarkModel
            {
                moneyTypeId = parentType.moneyTypeId,
                remark = typeName
            });

            return MapSubMoneyType(createdSubType, parentType);
        }

        public GetMoneyTypeCategoryResponse UpdateSubMoneyType(string userEmail, int subCategoryId, UpdateSubMoneyTypeRequest updateSubMoneyTypeRequest)
        {
            var subCategory = _typeRepo.GetTypeRemarkDetailById(userEmail, subCategoryId);
            if (subCategory == null)
            {
                throw new KeyNotFoundException("Subcategory not found");
            }

            var parentType = _typeRepo.GetMoneyTypeById(userEmail, subCategory.moneyTypeId);
            if (parentType == null)
            {
                throw new KeyNotFoundException("Parent category not found");
            }

            subCategory.remark = RequireValue(updateSubMoneyTypeRequest.typeName, "Type name is required");
            var updatedSubCategory = _typeRepo.UpdateTypeRemark(subCategory);
            return MapSubMoneyType(updatedSubCategory, parentType);
        }

        public bool DeleteMoneyType(string userEmail, int typeId)
        {
            if (typeId <= 0)
            {
                throw new ArgumentException("Category id is required");
            }

            return _typeRepo.DeleteMoneyType(userEmail, typeId);
        }

        public bool DeleteSubMoneyType(string userEmail, int subCategoryId)
        {
            if (subCategoryId <= 0)
            {
                throw new ArgumentException("Subcategory id is required");
            }

            return _typeRepo.DeleteSubMoneyType(userEmail, subCategoryId);
        }

        public bool RestoreMoneyType(string userEmail, int typeId)
        {
            if (typeId <= 0)
            {
                throw new ArgumentException("Category id is required");
            }

            return _typeRepo.RestoreMoneyType(userEmail, typeId);
        }

        public bool RestoreSubMoneyType(string userEmail, int subCategoryId)
        {
            if (subCategoryId <= 0)
            {
                throw new ArgumentException("Subcategory id is required");
            }

            return _typeRepo.RestoreSubMoneyType(userEmail, subCategoryId);
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
                moneyTypeDetailId = updateTypePay.moneyTypeDetailId,
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

        private static string? NormalizeCategoryType(string? categoryType)
        {
            if (string.IsNullOrWhiteSpace(categoryType))
            {
                return null;
            }

            if (categoryType.Equals("expense", StringComparison.OrdinalIgnoreCase))
            {
                return "Expense";
            }

            if (categoryType.Equals("income", StringComparison.OrdinalIgnoreCase))
            {
                return "Income";
            }

            return null;
        }

        private static string RequireValue(string? value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(errorMessage);
            }

            return value.Trim();
        }

        private GetMoneyTypeCategoryResponse MapMoneyTypeWithChildren(MoneyTypesModel moneyType)
        {
            var children = _typeRepo.GetTypeRemarkById(moneyType.moneyTypeId)
                .OrderBy(remark => remark.remarkId)
                .Select(remark => MapSubMoneyType(remark, moneyType))
                .ToList();

            return new GetMoneyTypeCategoryResponse
            {
                id = moneyType.moneyTypeId,
                name = moneyType.typeName ?? string.Empty,
                icon = moneyType.icon,
                color = moneyType.color,
                categoryType = moneyType.categoryType ?? string.Empty,
                isDeleted = moneyType.isDeleted,
                children = children
            };
        }

        private GetMoneyTypeCategoryResponse MapDeletedMoneyTypeWithChildren(MoneyTypesModel moneyType)
        {
            var children = _typeRepo.GetDeletedTypeRemarkById(moneyType.moneyTypeId)
                .OrderBy(remark => remark.remarkId)
                .Select(remark => MapSubMoneyType(remark, moneyType))
                .ToList();

            return new GetMoneyTypeCategoryResponse
            {
                id = moneyType.moneyTypeId,
                name = moneyType.typeName ?? string.Empty,
                icon = moneyType.icon,
                color = moneyType.color,
                categoryType = moneyType.categoryType ?? string.Empty,
                isDeleted = moneyType.isDeleted,
                children = children
            };
        }

        private static GetMoneyTypeCategoryResponse MapSubMoneyType(
            MoneyTypeDetailRemarkModel typeRemark,
            MoneyTypesModel parentType)
        {
            return new GetMoneyTypeCategoryResponse
            {
                id = typeRemark.remarkId,
                name = typeRemark.remark,
                icon = parentType.icon,
                color = parentType.color,
                categoryType = parentType.categoryType ?? string.Empty,
                isDeleted = typeRemark.isDeleted
            };
        }
    }
}
