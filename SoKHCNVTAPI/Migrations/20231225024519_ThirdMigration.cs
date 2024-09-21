using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SoKHCNVTAPI.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "StepStatus",
                schema: "skhcn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StepId = table.Column<long>(type: "bigint", nullable: false),
                    ModuleId = table.Column<long>(type: "bigint", nullable: true),
                    TargetId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepStatus_Steps_StepId",
                        column: x => x.StepId,
                        principalSchema: "skhcn",
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

         
        }
    }
}
