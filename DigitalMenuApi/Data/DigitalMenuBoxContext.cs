﻿
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Data
{
    public partial class DigitalMenuBoxContext : DbContext
    {
        public DigitalMenuBoxContext()
        {
        }

        public DigitalMenuBoxContext(DbContextOptions<DigitalMenuBoxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountRole> AccountRole { get; set; }
        public virtual DbSet<Box> Box { get; set; }
        public virtual DbSet<BoxType> BoxType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductList> ProductList { get; set; }
        public virtual DbSet<ProductListProduct> ProductListProduct { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<ScreenTemplate> ScreenTemplate { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Template> Template { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountRoleConfiguration());
            modelBuilder.ApplyConfiguration(new BoxConfiguration());
            modelBuilder.ApplyConfiguration(new BoxTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductListConfiguration());
            modelBuilder.ApplyConfiguration(new ProductListProductConfiguration());
            modelBuilder.ApplyConfiguration(new ScreenConfiguration());
            modelBuilder.ApplyConfiguration(new ScreenTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
