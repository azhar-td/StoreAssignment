using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAssignment.Models
{
    public class StoreDBContext: DbContext
    {
        public StoreDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                Id=1,
                Name = "Samsung",
            }, new Brand
            {
                Id=2,
                Name = "PEL",
            },
            new Brand
            {
                Id=3,
                Name = "Sony",
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id=1,
                Name = "Electronics",
            }, new Category
            {
                Id=2,
                Name = "Mobiles",
            });
            modelBuilder.Entity<SubCategory>().HasData(new SubCategory
            {
                Id=1,
                CategoryId=1,
                Name = "TV",
            },
            new SubCategory
            {
                Id = 2,
                CategoryId = 1,
                Name = "Air Conditioned",
            },
            new SubCategory
            {
                Id = 3,
                CategoryId = 1,
                Name = "Fans",
            }
            , new SubCategory
            {
                Id=4,
                CategoryId=2,
                Name = "Mobile J7",
            },
             new SubCategory
             {
                 Id = 5,
                 CategoryId = 2,
                 Name = "Tab 5.0",
             }
             , new SubCategory
             {
                 Id = 6,
                 CategoryId = 2,
                 Name = "Airpods",
             });
        }
    }
}
