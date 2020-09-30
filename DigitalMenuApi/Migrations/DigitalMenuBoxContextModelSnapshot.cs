﻿// <auto-generated />
using System;
using DigitalMenuApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalMenuApi.Migrations
{
    [DbContext(typeof(DigitalMenuBoxContext))]
    partial class DigitalMenuBoxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DigitalMenuApi.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StoreId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.AcountRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AcountRole");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Box", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoxTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("FooterId")
                        .HasColumnType("int");

                    b.Property<int?>("HeaderId")
                        .HasColumnType("int");

                    b.Property<int?>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Src")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoxTypeId");

                    b.HasIndex("FooterId");

                    b.HasIndex("HeaderId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Box");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.BoxType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("BoxType");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Src")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.ProductList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoxId")
                        .HasColumnType("int");

                    b.Property<int?>("MaxSize")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.ToTable("ProductList");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.ProductListProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Location")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductListId");

                    b.ToTable("ProductListProduct");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Screen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnName("UId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Screen");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.ScreenTemplate", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ScreenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScreenId");

                    b.HasIndex("TemplateId");

                    b.ToTable("ScreenTemplate");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Src")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("Uilink")
                        .HasColumnName("UILink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Account", b =>
                {
                    b.HasOne("DigitalMenuApi.Models.AcountRole", "Role")
                        .WithMany("Account")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Account_AcountRole")
                        .IsRequired();

                    b.HasOne("DigitalMenuApi.Models.Store", "Store")
                        .WithMany("Account")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Account_Store")
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Box", b =>
                {
                    b.HasOne("DigitalMenuApi.Models.BoxType", "BoxType")
                        .WithMany("Box")
                        .HasForeignKey("BoxTypeId")
                        .HasConstraintName("FK_Box_ContainerType");

                    b.HasOne("DigitalMenuApi.Models.Session", "Footer")
                        .WithMany("BoxFooter")
                        .HasForeignKey("FooterId")
                        .HasConstraintName("FK_Box_Session1");

                    b.HasOne("DigitalMenuApi.Models.Session", "Header")
                        .WithMany("BoxHeader")
                        .HasForeignKey("HeaderId")
                        .HasConstraintName("FK_Box_Session");

                    b.HasOne("DigitalMenuApi.Models.Template", "Template")
                        .WithMany("Box")
                        .HasForeignKey("TemplateId")
                        .HasConstraintName("FK_Container_Template")
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalMenuApi.Models.ProductList", b =>
                {
                    b.HasOne("DigitalMenuApi.Models.Box", "Box")
                        .WithMany("ProductList")
                        .HasForeignKey("BoxId")
                        .HasConstraintName("FK_ProductList_Box");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.ProductListProduct", b =>
                {
                    b.HasOne("DigitalMenuApi.Models.Product", "Product")
                        .WithMany("ProductListProduct")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductListProduct_Product");

                    b.HasOne("DigitalMenuApi.Models.ProductList", "ProductList")
                        .WithMany("ProductListProduct")
                        .HasForeignKey("ProductListId")
                        .HasConstraintName("FK_ProductListProduct_ProductList");
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Screen", b =>
                {
                    b.HasOne("DigitalMenuApi.Models.Store", "Store")
                        .WithMany("Screen")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Screen_Store")
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalMenuApi.Models.ScreenTemplate", b =>
                {
                    b.HasOne("DigitalMenuApi.Models.Screen", "Screen")
                        .WithMany("ScreenTemplate")
                        .HasForeignKey("ScreenId")
                        .HasConstraintName("FK_ScreenTemplate_Screen")
                        .IsRequired();

                    b.HasOne("DigitalMenuApi.Models.Template", "Template")
                        .WithMany("ScreenTemplate")
                        .HasForeignKey("TemplateId")
                        .HasConstraintName("FK_ScreenTemplate_Template")
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalMenuApi.Models.Template", b =>
                {
                    b.HasOne("DigitalMenuApi.Models.Store", "Store")
                        .WithMany("Template")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Template_Store");
                });
#pragma warning restore 612, 618
        }
    }
}
