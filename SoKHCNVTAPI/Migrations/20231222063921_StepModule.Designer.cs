﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SoKHCNVTAPI.Configurations;

#nullable disable

namespace SoKHCNVTAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231222063921_StepModule")]
    partial class StepModule
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);


            modelBuilder.Entity("SoKHCNVTAPI.Entities.Step", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<short?>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("Tag")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Steps", "skhcn");
                });
            modelBuilder.Entity("SoKHCNVTAPI.Entities.Actions", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                b.Property<DateTime?>("CreatedAt")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("Description")
                    .HasMaxLength(200)
                    .HasColumnType("character varying(200)");

                b.Property<bool?>("IsDeleted")
                    .HasColumnType("boolean");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<long>("RoleId")
                    .HasColumnType("bigint");

                b.Property<short?>("Status")
                    .HasColumnType("smallint");

                b.Property<string>("Tag")
                    .HasColumnType("text");

                b.Property<DateTime?>("UpdatedAt")
                    .HasColumnType("timestamp without time zone");

                b.HasKey("Id");

                b.ToTable("Actions", "skhcn");
            });

            modelBuilder.Entity("SoKHCNVTAPI.Entities.StepUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<short?>("Status")
                        .HasColumnType("smallint");

                    b.Property<long>("StepId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("StepUser", "skhcn");
                });

#pragma warning restore 612, 618
        }
    }
}
