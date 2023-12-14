using KontaktyAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontaktyAPI.Infrastructure
{
    public static class Seed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>(c =>
            {
                c.HasData(new Category()
                {
                    Id = 1,
                    Name = "Company",
                });


                c.HasData(new Category()
                {
                    Id = 2,
                    Name = "Private",
                });


                c.HasData(new Category()
                {
                    Id = 3,
                    Name = "Other",
                });

            });

            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory() { Id = 1, Name = "Coworker", CategoryId = 1 },
                new SubCategory() { Id = 2, Name = "Boss", CategoryId = 1 },
                new SubCategory() { Id = 3, Name = "Client", CategoryId = 1 },
                new SubCategory() { Id = 4, Name = "Manager", CategoryId = 1 },
                new SubCategory() { Id = 5, Name = "Private", CategoryId = 2 }
            );
        }
    }
}

