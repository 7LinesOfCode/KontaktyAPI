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
        public ListContactForListVm GetContacts(int pageSize, int pageNo, string? searchString, int? selectedCategoryId);
        public ContactDetailsVm GetContact(int id);
        public IEnumerable<CategoryVm> GetCategories();
        public IEnumerable<SubCategoryVm> GetSubCategories();
        public int CreateSubCategory(SubCategoryVm subCategory);
        public int CreateContact(ContactDetailsVm contactDetails);
        public bool DeleteContact(int id);
        public bool EditContact(int id,ContactDetailsVm contactDetails);
    }
}
