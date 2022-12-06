﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forte.EpiserverRedirects.SqlServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RedirectRules",
                columns: table => new
                {
                    RuleId = table.Column<string>(type: "uniqueidentifier", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: true),
                    OldPattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewPattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectRuleType = table.Column<int>(type: "int", nullable: false),
                    RedirectType = table.Column<int>(type: "int", nullable: false),
                    RedirectOrigin = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedirectRules", x => x.RuleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedirectRules_RuleId",
                table: "RedirectRules",
                column: "RuleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedirectRules");
        }
    }
}
