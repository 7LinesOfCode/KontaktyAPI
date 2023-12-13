using AutoMapper;
using AutoMapper.QueryableExtensions;
using KontaktyAPI.Application.Interfaces;
using KontaktyAPI.Application.Viewmodels.Collections;
using KontaktyAPI.Application.Viewmodels.Single;
using KontaktyAPI.Domain.Entities;
using KontaktyAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _repo;
        public ContactService(IMapper mapper, IContactRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public ContactDetailsVm GetContact(int id)
        {
            var contact =  _repo.GetContact(id);
            var contactVm = _mapper.Map<ContactDetailsVm>(contact);
            return contactVm;
        }

        public ListContactForListVm GetContacts(int pageSize, int pageNo, string? searchString, int? selectedCategoryId)
        {
            var query = _repo.GetContacts(); 

            var categoriesQuery = _repo.GetCategories(); 

            List<string> Categories = new List<string>();

            foreach (var category in categoriesQuery)
            {
                if(category != null && !Categories.Contains(category.Name))
                {
                    Categories.Add(category.Name);
                }
            }

            if (selectedCategoryId != null)
            {
                query = query.Where(c => c.CategoryId == selectedCategoryId);
            }

            if (searchString == null || searchString == string.Empty)
            {
                searchString = "";
            }

            var contacts = query.Where(c=>(c.FirstName + c.LastName).Contains(searchString)).ProjectTo<ContactForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var contactsToShow = contacts.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            var ContactList = new ListContactForListVm()
            {
                PageSize = pageSize,
                CurrentylPage = pageNo,
                SearchString = searchString,
                Contacts = contactsToShow,
                Categories = Categories,
                Count = contacts.Count,
                SelectedCategoryId = selectedCategoryId
            };

            return ContactList;
        }

        public IEnumerable<CategoryVm> GetCategories()
        {
            var categories = _repo.GetCategories().ProjectTo<CategoryVm>(_mapper.ConfigurationProvider).ToList();

            return categories;
        }

        public IEnumerable<SubCategoryVm> GetSubCategories()
        {
            var subCategories = _repo.GetSubCategories().ProjectTo<SubCategoryVm>(_mapper.ConfigurationProvider).ToList();
            return subCategories;
        }

        public int CreateSubCategory(SubCategoryVm subCategory)
        {
            var newSubCategory =_mapper.Map<SubCategory>(subCategory);
            var id =  _repo.AddNewSubCategory(newSubCategory);
            return id;
        }

        public int CreateContact(ContactDetailsVm contactDetails)
        {
            var newContact = _mapper.Map<Contact>(contactDetails);
            var id = _repo.AddNewContact(newContact);
            return id;
        }

        public bool DeleteContact(int id)
        {
             var response = _repo.DeleteContact(id);
            return response;
        }

        public bool EditContact(int id,ContactDetailsVm contactDetails)
        {
            var contactToUpdate = _repo.GetContact(id);
            if (contactToUpdate == null)
            {
                return false;
            }

            var EdittedContact = _mapper.Map<Contact>(contactDetails);
            _repo.UpdateContact(id, EdittedContact);
            return true;
        }
    }
}
