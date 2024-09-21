using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoKHCNVTAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "skhcn");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TaxNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    Address = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    Ward = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EnglishName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Extension = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Path = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpertIdentifiers",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Degree = table.Column<string>(type: "text", nullable: true),
                    AcademicRank = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Province = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileStatuss",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStatuss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Parent = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionIdentifiers",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionLevels",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionStatuses",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionTypes",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficerIdentifiers",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficerIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Officers",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    JoinAd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    Supervisor = table.Column<long>(type: "bigint", nullable: false),
                    Remark = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationIdentifiers",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnershipForms",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnershipRepresentatives",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    ApprovedBy = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipRepresentatives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParticipationRoles",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicationTypes",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchFieldIds",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchFieldIds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Password = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RefreshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: true),
                    ResetPasswordCode = table.Column<string>(type: "text", nullable: true),
                    NationalId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Province = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    District = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ward = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Position = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Remark = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    ExpertId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalSchema: "skhcn",
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "skhcn",
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationIdentifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationIdentifiers_OrganizationIdentifie~",
                        column: x => x.OrganizationIdentifierId,
                        principalSchema: "skhcn",
                        principalTable: "OrganizationIdentifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationTypes_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalSchema: "skhcn",
                        principalTable: "OrganizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "skhcn",
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MissionNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MissionLevelId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    ResearchFieldId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectTypeId = table.Column<long>(type: "bigint", nullable: true),
                    TotalTimeWithMonth = table.Column<int>(type: "integer", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalExpenses = table.Column<decimal>(type: "numeric", nullable: true),
                    TotalExpensesInWords = table.Column<string>(type: "text", nullable: true),
                    GovernmentExpenses = table.Column<decimal>(type: "numeric", nullable: true),
                    SelfExpenses = table.Column<decimal>(type: "numeric", nullable: true),
                    OtherExpenses = table.Column<decimal>(type: "numeric", nullable: true),
                    Keywords = table.Column<string>(type: "text", nullable: true),
                    ResearchObjective = table.Column<string>(type: "text", nullable: true),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    AnticipatedProduct = table.Column<string>(type: "text", nullable: true),
                    ApplicationScopeAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DecisionCode = table.Column<string>(type: "text", nullable: true),
                    DecisionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReportingYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReportCode = table.Column<string>(type: "text", nullable: true),
                    AcceptanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResearchRegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    ConsolidatedFile = table.Column<string>(type: "text", nullable: true),
                    SummaryFile = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ApplicationAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    EconomicEfficiency = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_MissionLevels_MissionLevelId",
                        column: x => x.MissionLevelId,
                        principalSchema: "skhcn",
                        principalTable: "MissionLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "skhcn",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_ProjectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalSchema: "skhcn",
                        principalTable: "ProjectTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_ResearchFieldIds_ResearchFieldId",
                        column: x => x.ResearchFieldId,
                        principalSchema: "skhcn",
                        principalTable: "ResearchFieldIds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wards_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "skhcn",
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "Degrees",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3138), "Học vị 01", false, "Tú tài", (short)1, null },
                    { 2L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3155), "Cử nhân", false, "Cử nhân", (short)1, null },
                    { 3L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3156), "Giáo sư", false, "Giáo sư", (short)1, null },
                    { 4L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3158), "Phó giáo sư", false, "Phó giáo sư", (short)1, null },
                    { 5L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3159), "Tiến sĩ", false, "Tiến sĩ", (short)1, null },
                    { 6L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3160), "Thạc sĩ", false, "Thạc sĩ", (short)1, null },
                    { 7L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3161), "Kỹ sư", false, "Kỹ sư", (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3203), "SỞ KHOA HỌC VÀ CÔNG NGHỆ TỈNH BÀ RỊA - VŨNG TÀU", false, "SỞ KHOA HỌC VÀ CÔNG NGHỆ TỈNH BÀ RỊA - VŨNG TÀU", (short)1, null },
                    { 2L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3204), "TRUNG TÂM THÔNG TIN VÀ ỨNG DỤNG KHOA HỌC & CÔNG NGHỆ TỈNH BÀ RỊA – VŨNG TÀU", false, "TRUNG TÂM THÔNG TIN VÀ ỨNG DỤNG KHOA HỌC & CÔNG NGHỆ TỈNH BÀ RỊA – VŨNG TÀU", (short)1, null },
                    { 3L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3205), "CHI CỤC TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG", false, "CHI CỤC TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG", (short)1, null },
                    { 4L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3206), "TRUNG TÂM KỸ THUẬT TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG", false, "TRUNG TÂM KỸ THUẬT TIÊU CHUẨN ĐO LƯỜNG CHẤT LƯỢNG", (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "ExpertIdentifiers",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "IsDeleted", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, "V.05.01.01", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3228), "định danh chuyên gia", false, "Nghiên cứu viên cao cấp (hạng I)", (short)1, null },
                    { 2L, "V.05.01.02", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3229), "Nghiên cứu viên chính (hạng II)", false, "Nghiên cứu viên chính (hạng II)", (short)1, null },
                    { 3L, "V.05.01.03", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3230), "Nghiên cứu viên (hạng III)", false, "Nghiên cứu viên (hạng III)", (short)1, null },
                    { 4L, "V.05.01.04", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3231), "Trợ lý nghiên cứu (hạng IV)", false, "Trợ lý nghiên cứu (hạng IV)", (short)1, null },
                    { 5L, "V.05.02.05", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3232), "Kỹ sư cao cấp (hạng I)", false, "Kỹ sư cao cấp (hạng I)", (short)1, null },
                    { 6L, "V.05.02.06", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3233), "Kỹ sư chính (hạng II)", false, "Kỹ sư chính (hạng II)", (short)1, null },
                    { 7L, "V.05.02.07", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3234), "Kỹ sư (hạng III)", false, "Kỹ sư (hạng III)", (short)1, null },
                    { 8L, "V.05.02.08", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3235), "Kỹ thuật viên", false, "Kỹ thuật viên", (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 11, 13, 10, 7, 56, 435, DateTimeKind.Local).AddTicks(8161), "Group Super Administrator", false, "Super Administrator", (short)1, null },
                    { 2L, new DateTime(2023, 11, 13, 10, 7, 56, 435, DateTimeKind.Local).AddTicks(8176), "Group Administrator", false, "Administrator", (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "MissionIdentifiers",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "IsDeleted", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, "CT", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3253), "CT là ký hiệu chung cho các chương trình khoa học và công nghệ cấp Bộ.", false, "CT", (short)1, null },
                    { 2L, "DT", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3254), "ĐT là ký hiệu chung cho các đề tài khoa học và công nghệ cấp Bộ.", false, "DT", (short)1, null },
                    { 3L, "DA", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3256), "DA là ký hiệu chung cho các dự án khoa học và công nghệ cấp Bộ.", false, "DA", (short)1, null },
                    { 4L, "NVK", new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3256), "NVK là ký hiệu chung cho các nhiệm vụ khoa học và công nghệ cấp Bộ khác.", false, "NVK", (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "MissionStatuses",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3310), "Nháp", false, "Nháp", (short)1, null },
                    { 2L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3311), "Trình duyệt", false, "Trình duyệt", (short)1, null },
                    { 3L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3317), "Xác nhận đã nhận nhiêm vụ", false, "Xác nhận", (short)1, null },
                    { 4L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3326), "Đang xử lý", false, "Đang xử lý", (short)1, null },
                    { 5L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3341), "Hoàn thành", false, "Hoàn thành", (short)1, null },
                    { 6L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3341), "Huỷ", false, "Huỷ", (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "PublicationTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3287), "Quốc tế", false, "Quốc tế", (short)1, null },
                    { 2L, new DateTime(2023, 11, 13, 10, 7, 56, 650, DateTimeKind.Local).AddTicks(3288), "Trong nước", false, "Trong nước", (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "skhcn",
                table: "Users",
                columns: new[] { "Id", "Address", "Code", "CreatedAt", "CreatedBy", "District", "Email", "ExpertId", "FirstName", "GroupId", "IsDeleted", "IsLocked", "LastLogin", "LastName", "NationalId", "Password", "Phone", "Position", "Province", "RefreshToken", "Remark", "ResetPasswordCode", "Role", "Status", "Token", "UpdatedAt", "UpdatedBy", "Ward" },
                values: new object[,]
                {
                    { 1L, null, "KHCN0000", new DateTime(2023, 11, 13, 10, 7, 56, 435, DateTimeKind.Local).AddTicks(8354), 0L, null, "ntcnet@gmail.com", null, "Nguyen", 1L, false, false, null, "Cong", "1000000001", "$2b$10$YXobAn0trhOuZ8FX3oVxyu2m9jO8I7FOtkUXe5yAfRFwk5a35lzTi", "+84986435388", null, null, null, null, null, null, (short)1, null, null, null, null },
                    { 2L, null, "KHCN0001", new DateTime(2023, 11, 13, 10, 7, 56, 505, DateTimeKind.Local).AddTicks(5587), 0L, null, "hai.hoang.2762@gmail.com", null, "Hoang", 1L, false, false, null, "Hai", "1000000001", "$2b$10$snem90wg2w9DYO7HxD3touzHu.vPBSYJ2pL/Ui3rsgQC8tu53LWBm", "+84379813013", null, null, null, null, null, null, (short)1, null, null, null, null },
                    { 3L, null, "KHCN0003", new DateTime(2023, 11, 13, 10, 7, 56, 579, DateTimeKind.Local).AddTicks(2162), 0L, null, "thuyetlam@gmail.com", null, "Thuyet", 1L, false, false, null, "Lam", "1000000001", "$2b$10$sXY5sr7oIJ4pyu/iIZImnOWugBCVOGbPt6ZxUKFjvS1biwxZIf4XW", "+84999999999", null, null, null, null, null, null, (short)1, null, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                schema: "skhcn",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationIdentifierId",
                schema: "skhcn",
                table: "Organizations",
                column: "OrganizationIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationTypeId",
                schema: "skhcn",
                table: "Organizations",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MissionLevelId",
                schema: "skhcn",
                table: "Tasks",
                column: "MissionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OrganizationId",
                schema: "skhcn",
                table: "Tasks",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectTypeId",
                schema: "skhcn",
                table: "Tasks",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResearchFieldId",
                schema: "skhcn",
                table: "Tasks",
                column: "ResearchFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExpertId",
                schema: "skhcn",
                table: "Users",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                schema: "skhcn",
                table: "Users",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictId",
                schema: "skhcn",
                table: "Wards",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Degrees",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "EducationLevels",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "ExpertIdentifiers",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "FileStatuss",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "MissionIdentifiers",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "MissionStatuses",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "MissionTypes",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Nationalities",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "OfficerIdentifiers",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Officers",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "OwnershipForms",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "OwnershipRepresentatives",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "ParticipationRoles",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "PublicationTypes",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Titles",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Wards",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "MissionLevels",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "ProjectTypes",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "ResearchFieldIds",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Experts",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Districts",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "OrganizationIdentifiers",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "OrganizationTypes",
                schema: "skhcn");

            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "skhcn");
        }
    }
}
