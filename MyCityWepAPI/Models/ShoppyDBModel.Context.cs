﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCityWepAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShoppyDBEntities : DbContext
    {
        public ShoppyDBEntities()
            : base("name=ShoppyDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblCity> tblCities { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblShopImage> tblShopImages { get; set; }
        public virtual DbSet<tblShop> tblShops { get; set; }
        public virtual DbSet<tblState> tblStates { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblShopProductImage> tblShopProductImages { get; set; }
        public virtual DbSet<tblShopProduct> tblShopProducts { get; set; }
        public virtual DbSet<tblBanner> tblBanners { get; set; }
    }
}
