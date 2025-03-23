using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Shipping.repo.ShippingCon
{
    public class ShippingContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Governorate> Governorates { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<SpecialPrice> SpecialPrices { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<Representative> Representatives { get; set; }
        public virtual DbSet<RepresentativeGovernorate> RepresentativeGovernorates { get; set; }
        public virtual DbSet<Rejection> Rejections { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Weight> Weights { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<DeliverToVillage> DeliverToVillages { get; set; }
        public virtual DbSet<GroupPermission> GroupPermissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }







        public ShippingContext(DbContextOptions options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Discriminator configuration
            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Merchant>("Merchant")
                .HasValue<Employee>("Employee")
                .HasValue<Representative>("Representative");

            modelBuilder.Entity<Order>()
               .HasOne(o => o.Governorate)
               .WithMany(g => g.Orders)
               .HasForeignKey(o => o.GovernorateId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Branch>()
               .HasMany(b => b.Orders).WithOne(c => c.Branch)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Group>()
               .HasMany(g => g.GroupPermissions)
               .WithOne(gp => gp.Group)
               .HasForeignKey(gp => gp.GroupId);

            modelBuilder.Entity<Permission>()
                .HasMany(p => p.GroupPermissions)
                .WithOne(gp => gp.Permission)
                .HasForeignKey(gp => gp.PermissionId);


            modelBuilder.Entity<Group>()
              .HasMany(g => g.Employees)
              .WithOne(e => e.Group)
              .HasForeignKey(e => e.GroupId);

            // تعريف البيانات بشكل مباشر داخل `HasData`
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "Branch" },
                new Permission { Id = 2, Name = "City" },
                new Permission { Id = 3, Name = "Governorate" },
                new Permission { Id = 4, Name = "Employee" },
                new Permission { Id = 5, Name = "Representative" },
                new Permission { Id = 6, Name = "Merchant" },
                new Permission { Id = 7, Name = "Order" },
                new Permission { Id = 8, Name = "OrderReports" },
                new Permission { Id = 9, Name = "Group" },
                new Permission { Id = 10, Name = "ReasonsRefusalType" },
                new Permission { Id = 11, Name = "ShippingType" },
                new Permission { Id = 12, Name = "DeliverToVillage" },
                new Permission { Id = 13, Name = "Weight" }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "ادمن", Date = new DateTime(2024, 03, 23, 12, 0, 0) }
            );

            modelBuilder.Entity<Branch>().HasData(
                new Branch { Id = 1, Name = "الفرع الرئيسي" , DateTime = new DateTime(2024, 03, 23, 12, 0, 0) }
            );

            modelBuilder.Entity<Weight>().HasData(
                new Weight { Id = 1, DefaultWeight = 10, AdditionalPrice = 5 }
            );

            modelBuilder.Entity<DeliverToVillage>().HasData(
                new DeliverToVillage { Id = 1, AdditionalCost = 10 }
            );

            modelBuilder.Entity<GroupPermission>().HasData(
                new GroupPermission { id = 1, GroupId = 1, PermissionId = 1, Action = "Add" },
                new GroupPermission { id = 2, GroupId = 1, PermissionId = 1, Action = "Edit" },
                new GroupPermission { id = 3, GroupId = 1, PermissionId = 1, Action = "Delete" },
                new GroupPermission { id = 4, GroupId = 1, PermissionId = 1, Action = "Show" },
               new GroupPermission { id = 5, GroupId = 1, PermissionId = 2, Action = "Add" },
            new GroupPermission { id = 6, GroupId = 1, PermissionId = 2, Action = "Edit" },
            new GroupPermission { id = 7, GroupId = 1, PermissionId = 2, Action = "Delete" },
            new GroupPermission { id = 8, GroupId = 1, PermissionId = 2, Action = "Show" },

            new GroupPermission { id = 9, GroupId = 1, PermissionId = 3, Action = "Add" },
            new GroupPermission { id = 10, GroupId = 1, PermissionId = 3, Action = "Edit" },
            new GroupPermission { id = 11, GroupId = 1, PermissionId = 3, Action = "Delete" },
            new GroupPermission { id = 12, GroupId = 1, PermissionId = 3, Action = "Show" },

            new GroupPermission { id = 13, GroupId = 1, PermissionId = 4, Action = "Add" },
            new GroupPermission { id = 14, GroupId = 1, PermissionId = 4, Action = "Edit" },
            new GroupPermission { id = 15, GroupId = 1, PermissionId = 4, Action = "Delete" },
            new GroupPermission { id = 16, GroupId = 1, PermissionId = 4, Action = "Show" },

            new GroupPermission { id = 17, GroupId = 1, PermissionId = 5, Action = "Add" },
            new GroupPermission { id = 18, GroupId = 1, PermissionId = 5, Action = "Edit" },
            new GroupPermission { id = 19, GroupId = 1, PermissionId = 5, Action = "Delete" },
            new GroupPermission { id = 20, GroupId = 1, PermissionId = 5, Action = "Show" },

            new GroupPermission { id = 21, GroupId = 1, PermissionId = 6, Action = "Add" },
            new GroupPermission { id = 22, GroupId = 1, PermissionId = 6, Action = "Edit" },
            new GroupPermission { id = 23, GroupId = 1, PermissionId = 6, Action = "Delete" },
            new GroupPermission { id = 24, GroupId = 1, PermissionId = 6, Action = "Show" },

            new GroupPermission { id = 25, GroupId = 1, PermissionId = 7, Action = "Add" },
            new GroupPermission { id = 26, GroupId = 1, PermissionId = 7, Action = "Edit" },
            new GroupPermission { id = 27, GroupId = 1, PermissionId = 7, Action = "Delete" },
            new GroupPermission { id = 28, GroupId = 1, PermissionId = 7, Action = "Show" },

            new GroupPermission { id = 29, GroupId = 1, PermissionId = 8, Action = "Add" },
            new GroupPermission { id = 30, GroupId = 1, PermissionId = 8, Action = "Edit" },
            new GroupPermission { id = 31, GroupId = 1, PermissionId = 8, Action = "Delete" },
            new GroupPermission { id = 32, GroupId = 1, PermissionId = 8, Action = "Show" },

            new GroupPermission { id = 33, GroupId = 1, PermissionId = 9, Action = "Add" },
            new GroupPermission { id = 34, GroupId = 1, PermissionId = 9, Action = "Edit" },
            new GroupPermission { id = 35, GroupId = 1, PermissionId = 9, Action = "Delete" },
            new GroupPermission { id = 36, GroupId = 1, PermissionId = 9, Action = "Show" },


            new GroupPermission { id = 37, GroupId = 1, PermissionId = 10, Action = "Add" },
            new GroupPermission { id = 38, GroupId = 1, PermissionId = 10, Action = "Edit" },
            new GroupPermission { id = 39, GroupId = 1, PermissionId = 10, Action = "Delete" },
            new GroupPermission { id = 40, GroupId = 1, PermissionId = 10, Action = "Show" },

            new GroupPermission { id = 41, GroupId = 1, PermissionId = 11, Action = "Add" },
            new GroupPermission { id = 42, GroupId = 1, PermissionId = 11, Action = "Edit" },
            new GroupPermission { id = 43, GroupId = 1, PermissionId = 11, Action = "Delete" },
            new GroupPermission { id = 44, GroupId = 1, PermissionId = 11, Action = "Show" },

            new GroupPermission { id = 45, GroupId = 1, PermissionId = 12, Action = "Add" },
            new GroupPermission { id = 46, GroupId = 1, PermissionId = 12, Action = "Edit" },
            new GroupPermission { id = 47, GroupId = 1, PermissionId = 12, Action = "Delete" },
            new GroupPermission { id = 48, GroupId = 1, PermissionId = 12, Action = "Show" },

            new GroupPermission { id = 49, GroupId = 1, PermissionId = 13, Action = "Add" },
            new GroupPermission { id = 50, GroupId = 1, PermissionId = 13, Action = "Edit" },
            new GroupPermission { id = 51, GroupId = 1, PermissionId = 13, Action = "Delete" },
            new GroupPermission { id = 52, GroupId = 1, PermissionId = 13, Action = "Show" }
            );
            base.OnModelCreating(modelBuilder);

           

           
        }

    }

}
