using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itunes_13.Migrations
{
    public partial class addSalestoSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sales",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sales",
                table: "Songs");
        }
    }
}
