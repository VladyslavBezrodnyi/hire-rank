using Microsoft.EntityFrameworkCore.Migrations;

namespace HireRank.Infrastructure.Migrations
{
    public partial class AddEmployerConfirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: false);
            migrationBuilder.Sql("UPDATE dbo.AspNetUsers SET IsConfirmed=0 WHERE Discriminator='Employer'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "AspNetUsers");
        }
    }
}
