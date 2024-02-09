﻿// <auto-generated />
using System;
using HwProj.SolutionsService.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HwProj.SolutionsService.API.Migrations
{
    [DbContext(typeof(SolutionContext))]
    partial class SolutionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HwProj.Models.SolutionsService.Solution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<string>("GithubUrl");

                    b.Property<long?>("GroupId");

                    b.Property<string>("LecturerComment");

                    b.Property<string>("LecturerId");

                    b.Property<DateTime>("PublicationDate");

                    b.Property<int>("Rating");

                    b.Property<int>("State");

                    b.Property<string>("StudentId");

                    b.Property<long>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Solutions");
                });
#pragma warning restore 612, 618
        }
    }
}
