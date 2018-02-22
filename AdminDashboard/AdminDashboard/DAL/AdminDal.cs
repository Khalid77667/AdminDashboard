using AdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdminDashboard.DAL
{
    public class AdminDal:DbContext
    {
        public DbSet<Vendor> vendors { get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<SubCategory> subcategory { get; set; }
        public DbSet<Product> product { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vendor>().ToTable("[YOL].[VendorMaster]");
            modelBuilder.Entity<Brand>().ToTable("[YOL].[BrandMaster]");
            modelBuilder.Entity<Category>().ToTable("[YOL].[CategoryMaster]");
            modelBuilder.Entity<SubCategory>().ToTable("[YOL].[SubCategoryMaster]");
            modelBuilder.Entity<Product>().ToTable("[YOL].[ProductMaster]");
            
        }
    }
}