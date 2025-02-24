﻿// <auto-generated />
using DsaJet.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DsaJet.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250222145216_Added-New-Entity")]
    partial class AddedNewEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DsaJet.Api.Entities.Prerequisite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Prereq")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProblemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.ToTable("Prerequisite");
                });

            modelBuilder.Entity("DsaJet.Api.Entities.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VideoSolutionUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("DsaJet.Api.Entities.Solution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProblemId")
                        .HasColumnType("int");

                    b.Property<string>("SolutionCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("DsaJet.Api.Entities.Prerequisite", b =>
                {
                    b.HasOne("DsaJet.Api.Entities.Problem", "Problem")
                        .WithMany("Prerequisites")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("DsaJet.Api.Entities.Solution", b =>
                {
                    b.HasOne("DsaJet.Api.Entities.Problem", "Problem")
                        .WithMany()
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("DsaJet.Api.Entities.Problem", b =>
                {
                    b.Navigation("Prerequisites");
                });
#pragma warning restore 612, 618
        }
    }
}
