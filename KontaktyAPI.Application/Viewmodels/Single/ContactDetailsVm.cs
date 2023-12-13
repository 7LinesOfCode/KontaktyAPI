using AutoMapper;
using KontaktyAPI.Application.Mapping;
using KontaktyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Application.Viewmodels.Single
{
    public class ContactDetailsVm : IMapFrom<Contact> /// able to create new one
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactDetailsVm>()
                .ReverseMap();
        }
    }
}
