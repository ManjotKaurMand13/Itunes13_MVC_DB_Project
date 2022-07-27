using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itunes_13.Migrations
{
    public partial class AddWalletToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Wallet",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wallet",
                table: "Users");
        }
    }
}
