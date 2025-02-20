﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using loja_api.Context;

#nullable disable

namespace loja_api.Migrations
{
    [DbContext(typeof(ContextDB))]
    partial class ContextDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("loja_api.Entities.Cupom", b =>
                {
                    b.Property<Guid>("CupomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("CupomId");

                    b.ToTable("Cupom");
                });

            modelBuilder.Entity("loja_api.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("loja_api.Entities.MarketCart", b =>
                {
                    b.Property<Guid>("MarketCartId")
                        .HasColumnType("TEXT");

                    b.PrimitiveCollection<string>("AttDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CupomId")
                        .HasColumnType("TEXT");

                    b.PrimitiveCollection<string>("IdProducts")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("MarketCartId");

                    b.HasIndex("CupomId")
                        .IsUnique();

                    b.ToTable("MarketCart");
                });

            modelBuilder.Entity("loja_api.Entities.Products", b =>
                {
                    b.Property<Guid>("IdProducts")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodeProduct")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantityStorage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeProduct")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdProducts");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("loja_api.Entities.Storage", b =>
                {
                    b.Property<Guid>("IdStorage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdProducts")
                        .HasColumnType("TEXT");

                    b.Property<double>("PriceBuy")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdStorage");

                    b.HasIndex("IdProducts");

                    b.ToTable("Storage");
                });

            modelBuilder.Entity("loja_api.Entities.User", b =>
                {
                    b.Property<Guid>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("loja_api.Entities.Cupom", b =>
                {
                    b.OwnsOne("loja_api.Entities.Auditable", "Auditable", b1 =>
                        {
                            b1.Property<Guid>("CupomId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("CreatebyId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("UpdateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("UpdatebyId")
                                .HasColumnType("TEXT");

                            b1.HasKey("CupomId");

                            b1.ToTable("Cupom");

                            b1.WithOwner()
                                .HasForeignKey("CupomId");
                        });

                    b.Navigation("Auditable")
                        .IsRequired();
                });

            modelBuilder.Entity("loja_api.Entities.Employee", b =>
                {
                    b.OwnsOne("loja_api.Entities.Auditable", "Auditable", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("INTEGER");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("CreatebyId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("UpdateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("UpdatebyId")
                                .HasColumnType("TEXT");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.Navigation("Auditable")
                        .IsRequired();
                });

            modelBuilder.Entity("loja_api.Entities.MarketCart", b =>
                {
                    b.HasOne("loja_api.Entities.Cupom", "Cupom")
                        .WithOne()
                        .HasForeignKey("loja_api.Entities.MarketCart", "CupomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("loja_api.Entities.User", "User")
                        .WithMany("MarketCart")
                        .HasForeignKey("MarketCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cupom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("loja_api.Entities.Products", b =>
                {
                    b.OwnsOne("loja_api.Entities.Auditable", "Auditable", b1 =>
                        {
                            b1.Property<Guid>("ProductsIdProducts")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("CreatebyId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("UpdateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("UpdatebyId")
                                .HasColumnType("TEXT");

                            b1.HasKey("ProductsIdProducts");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductsIdProducts");
                        });

                    b.Navigation("Auditable")
                        .IsRequired();
                });

            modelBuilder.Entity("loja_api.Entities.Storage", b =>
                {
                    b.HasOne("loja_api.Entities.Products", "Products")
                        .WithMany("Storages")
                        .HasForeignKey("IdProducts")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("loja_api.Entities.Auditable", "Auditable", b1 =>
                        {
                            b1.Property<Guid>("StorageIdStorage")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("CreatebyId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("UpdateDate")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("UpdatebyId")
                                .HasColumnType("TEXT");

                            b1.HasKey("StorageIdStorage");

                            b1.ToTable("Storage");

                            b1.WithOwner()
                                .HasForeignKey("StorageIdStorage");
                        });

                    b.Navigation("Auditable")
                        .IsRequired();

                    b.Navigation("Products");
                });

            modelBuilder.Entity("loja_api.Entities.Products", b =>
                {
                    b.Navigation("Storages");
                });

            modelBuilder.Entity("loja_api.Entities.User", b =>
                {
                    b.Navigation("MarketCart");
                });
#pragma warning restore 612, 618
        }
    }
}
