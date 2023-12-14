using KontaktyAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KontaktyAPI.Infrastructure
{
    public class dbContext : IdentityDbContext
    {

        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
            base.OnModelCreating(modelBuilder);
        }
    }
}
