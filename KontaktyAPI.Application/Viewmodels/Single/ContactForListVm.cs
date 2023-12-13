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
    public class ContactForListVm : IMapFrom<Contact>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactForListVm>()
                .ForMember(c=>c.FullName, opt => opt.MapFrom(c=>c.FirstName+" "+c.LastName))
                .ReverseMap();
        }
    }
}
