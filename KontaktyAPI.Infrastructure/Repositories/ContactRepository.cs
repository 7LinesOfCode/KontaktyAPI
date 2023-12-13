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

        public IQueryable<Contact> GetContacts()
        {
            return _context.Contacts;
        }

        public Contact GetContact(int id)
        {
            var contactModel = _context.Contacts.FirstOrDefault(c => c.Id == id);
            return contactModel;
        }

        public IQueryable<Category> GetCategories()
        {
            return _context.Categories;
        }

        public IQueryable<SubCategory> GetSubCategories()
        {
            return _context.SubCategories;
        }

        public int AddNewSubCategory(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            _context.SaveChanges();
            return subCategory.Id;
        }

        public int AddNewContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact.Id;
        }

        public void UpdateContact(int id,Contact UpdatedContact)
        {
            var dbContact = GetContact(id);
            if(dbContact != null)
            {
                dbContact.Id = id;
                dbContact.FirstName= UpdatedContact.FirstName;
                dbContact.LastName = UpdatedContact.LastName;
                dbContact.Email = UpdatedContact.Email;
                dbContact.BirthDay = UpdatedContact.BirthDay;
                dbContact.PhoneNumber = UpdatedContact.PhoneNumber;
                dbContact.CategoryId = UpdatedContact.CategoryId;
                dbContact.SubCategoryId = UpdatedContact.SubCategoryId;
                _context.SaveChanges();
            }
        }

        public bool DeleteContact(int id)
        {
            var contactToDelete = GetContact(id);
            if(contactToDelete != null)
            {
                _context.Contacts.Remove(contactToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
