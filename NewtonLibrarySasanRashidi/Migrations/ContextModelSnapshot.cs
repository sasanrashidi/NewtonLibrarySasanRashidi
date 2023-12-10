﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewtonLibrarySasanRashidi.Data;

#nullable disable

namespace NewtonLibrarySasanRashidi.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<int>("AuthorsId")
                        .HasColumnType("int");

                    b.Property<int>("BooksId")
                        .HasColumnType("int");

                    b.HasKey("AuthorsId", "BooksId");

                    b.HasIndex("BooksId");

                    b.ToTable("AuthorBook");
                });

            modelBuilder.Entity("NewtonLibrarySasanRashidi.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("NewtonLibrarySasanRashidi.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("BookIsLoaned")
                        .HasColumnType("bit")
                        .HasColumnName("Loaned");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<Guid>("Isbn")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserCardId")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserCardId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("NewtonLibrarySasanRashidi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("userCardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userCardId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NewtonLibrarySasanRashidi.Models.UserCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Pin")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserCards");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("NewtonLibrarySasanRashidi.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewtonLibrarySasanRashidi.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewtonLibrarySasanRashidi.Models.Book", b =>
                {
                    b.HasOne("NewtonLibrarySasanRashidi.Models.UserCard", "UserCard")
                        .WithMany("Books")
                        .HasForeignKey("UserCardId");

                    b.Navigation("UserCard");
                });

            modelBuilder.Entity("NewtonLibrarySasanRashidi.Models.User", b =>
                {
                    b.HasOne("NewtonLibrarySasanRashidi.Models.UserCard", "userCard")
                        .WithMany()
                        .HasForeignKey("userCardId");

                    b.Navigation("userCard");
                });

            modelBuilder.Entity("NewtonLibrarySasanRashidi.Models.UserCard", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
