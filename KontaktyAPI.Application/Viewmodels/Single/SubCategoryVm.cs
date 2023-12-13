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
    public class SubCategoryVm : IMapFrom<SubCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubCategory, SubCategoryVm>()
                .ReverseMap();
        }
    }
}
