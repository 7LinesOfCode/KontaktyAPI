using KontaktyAPI.Domain.Entities;
using KontaktyAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly dbContext _context;

        public ContactRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Contact>> GetContactsAsync()
        {
            return await Task.FromResult(_context.Contacts.AsQueryable());
        }

        public async Task<Contact> GetContactAsync(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IQueryable<Category>> GetCategoriesAsync()
        {
            return await Task.FromResult(_context.Categories.AsQueryable());
        }

        public async Task<IQueryable<SubCategory>> GetSubCategoriesAsync()
        {
            return await Task.FromResult(_context.SubCategories.AsQueryable());
        }

        public async Task<int> AddNewSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return subCategory.Id;
        }

        public async Task<int> AddNewContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task UpdateContactAsync(int id, Contact UpdatedContact)
        {
            var dbContact = await GetContactAsync(id);
            if (dbContact != null)
            {
                dbContact.Id = id;
                dbContact.FirstName = UpdatedContact.FirstName;
                dbContact.LastName = UpdatedContact.LastName;
                dbContact.Email = UpdatedContact.Email;
                dbContact.BirthDay = UpdatedContact.BirthDay;
                dbContact.PhoneNumber = UpdatedContact.PhoneNumber;
                dbContact.CategoryId = UpdatedContact.CategoryId;
                dbContact.SubCategoryId = UpdatedContact.SubCategoryId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contactToDelete = await GetContactAsync(id);
            if (contactToDelete != null)
            {
                _context.Contacts.Remove(contactToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
