using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCoreProjesi.Migrations
{
    public partial class UserTableAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilImageFileName",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue:"noimage.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilImageFileName",
                table: "User");
        }
    }
}
