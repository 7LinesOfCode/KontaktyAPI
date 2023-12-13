using KontaktyAPI.Application.Viewmodels.Collections;
using KontaktyAPI.Application.Viewmodels.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Application.Interfaces
{
    public interface IContactService
    {
        Task<ListContactForListVm> GetContactsAsync(int pageSize, int pageNo, string? searchString, int? selectedCategoryId);
        Task<ContactDetailsVm> GetContactAsync(int id);
        Task<IEnumerable<CategoryVm>> GetCategoriesAsync();
        Task<IEnumerable<SubCategoryVm>> GetSubCategoriesAsync();
        Task<int> CreateSubCategoryAsync(SubCategoryVm subCategory);
        Task<int> CreateContactAsync(ContactDetailsVm contactDetails);
        Task<bool> DeleteContactAsync(int id);
        Task<bool> EditContactAsync(int id, ContactDetailsVm contactDetails);
    }
}
