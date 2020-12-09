using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HireRank.Infrastructure.Migrations
{
    public partial class UpdateVacancyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "vacancies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "vacancies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "vacancies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "vacancies");
        }
    }
}
