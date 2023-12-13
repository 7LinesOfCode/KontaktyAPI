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
    public class CategoryVm : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryVm>()
                .ReverseMap();
        }
    }
}
