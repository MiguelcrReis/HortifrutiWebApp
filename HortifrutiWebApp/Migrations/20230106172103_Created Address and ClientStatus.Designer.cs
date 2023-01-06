﻿// <auto-generated />
using System;
using HortifrutiWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HortifrutiWebApp.Migrations
{
    [DbContext(typeof(WebAppDbContext))]
    [Migration("20230106172103_Created Address and ClientStatus")]
    partial class CreatedAddressandClientStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HortifrutiWebApp.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ClientStatus")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(60) CHARACTER SET utf8mb4")
                        .HasMaxLength(60);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("IdClient");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("HortifrutiWebApp.Models.Product", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descrition")
                        .IsRequired()
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150) CHARACTER SET utf8mb4")
                        .HasMaxLength(150);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("IdProduct");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("HortifrutiWebApp.Models.Client", b =>
                {
                    b.OwnsOne("HortifrutiWebApp.Models.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ClientIdClient")
                                .HasColumnType("int");

                            b1.Property<string>("Cep")
                                .IsRequired()
                                .HasColumnType("varchar(8) CHARACTER SET utf8mb4")
                                .HasMaxLength(8);

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                                .HasMaxLength(80);

                            b1.Property<string>("Complement")
                                .IsRequired()
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.Property<string>("Neighborhood")
                                .IsRequired()
                                .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                                .HasMaxLength(80);

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                                .HasMaxLength(10);

                            b1.Property<string>("Reference")
                                .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                                .HasMaxLength(100);

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                                .HasMaxLength(2);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                                .HasMaxLength(100);

                            b1.HasKey("ClientIdClient");

                            b1.ToTable("Clients");

                            b1.WithOwner()
                                .HasForeignKey("ClientIdClient");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}