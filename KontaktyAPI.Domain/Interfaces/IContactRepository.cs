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
        ///READ
        public IQueryable<Contact> GetContacts();
        public Contact GetContact(int id);
        public IQueryable<Category> GetCategories();
        public IQueryable<SubCategory> GetSubCategories();
        ///CREATE
        public int AddNewSubCategory(SubCategory subCategory);
        public int AddNewContact(Contact contact);
        ///UPDATED
        public void UpdateContact(int id,Contact UpdatedContact);
        ///DELETE
        public bool DeleteContact(int id);
    }
}
