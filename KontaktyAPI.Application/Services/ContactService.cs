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

        public async Task<ContactDetailsVm> GetContactAsync(int id)
        {
            var contact = await _repo.GetContactAsync(id);
            var contactVm = _mapper.Map<ContactDetailsVm>(contact);
            return contactVm;
        }

        public async Task<ListContactForListVm> GetContactsAsync(int pageSize, int pageNo, string? searchString, int? selectedCategoryId)
        {
            var query = await _repo.GetContactsAsync();

            var categoriesQuery = await _repo.GetCategoriesAsync();

            List<string> Categories = new List<string>();

            foreach (var category in categoriesQuery)
            {
                if (category != null && !Categories.Contains(category.Name))
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

            var contacts = query
                .Where(c => (c.FirstName + c.LastName).Contains(searchString))
                .ProjectTo<ContactForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var contactsToShow = contacts.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            var contactList = new ListContactForListVm()
            {
                PageSize = pageSize,
                CurrentylPage = pageNo,
                SearchString = searchString,
                Contacts = contactsToShow,
                Categories = Categories,
                Count = contacts.Count,
                SelectedCategoryId = selectedCategoryId
            };

            return contactList;
        }

        public async Task<IEnumerable<CategoryVm>> GetCategoriesAsync()
        {
            var categories = await _repo.GetCategoriesAsync();
            var categoryVms = _mapper.Map<IEnumerable<CategoryVm>>(categories);
            return categoryVms.ToList();
        }

        public async Task<IEnumerable<SubCategoryVm>> GetSubCategoriesAsync()
        {
            var subCategories = await _repo.GetSubCategoriesAsync();
            var subCategoryVms = _mapper.Map<IEnumerable<SubCategoryVm>>(subCategories);
            return subCategoryVms.ToList();
        }

        public async Task<int> CreateSubCategoryAsync(SubCategoryVm subCategory)
        {
            var newSubCategory = _mapper.Map<SubCategory>(subCategory);
            var id = await _repo.AddNewSubCategoryAsync(newSubCategory);
            return id;
        }

        public async Task<int> CreateContactAsync(ContactDetailsVm contactDetails)
        {
            var newContact = _mapper.Map<Contact>(contactDetails);
            var id = await _repo.AddNewContactAsync(newContact);
            return id;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var response = await _repo.DeleteContactAsync(id);
            return response;
        }

        public async Task<bool> EditContactAsync(int id, ContactDetailsVm contactDetails)
        {
            var contactToUpdate = await _repo.GetContactAsync(id);
            if (contactToUpdate == null)
            {
                return false;
            }

            var editedContact = _mapper.Map<Contact>(contactDetails);
            await _repo.UpdateContactAsync(id, editedContact);
            return true;
        }
    }
}
