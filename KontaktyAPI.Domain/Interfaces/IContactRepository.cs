using KontaktyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Domain.Interfaces
{
    public interface IContactRepository
    {
        // READ
        Task<IQueryable<Contact>> GetContactsAsync();
        Task<Contact> GetContactAsync(int id);
        Task<IQueryable<Category>> GetCategoriesAsync();
        Task<IQueryable<SubCategory>> GetSubCategoriesAsync();

        // CREATE
        Task<int> AddNewSubCategoryAsync(SubCategory subCategory);
        Task<int> AddNewContactAsync(Contact contact);

        // UPDATE
        Task UpdateContactAsync(int id, Contact updatedContact);

        // DELETE
        Task<bool> DeleteContactAsync(int id);
    }
}
