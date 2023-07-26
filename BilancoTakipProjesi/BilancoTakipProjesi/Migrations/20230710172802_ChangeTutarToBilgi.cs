using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilancoTakipProjesi.Migrations
{
    public partial class ChangeTutarToBilgi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Not",
                table: "Islems",
                newName: "Bilgi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bilgi",
                table: "Islems",
                newName: "Not");
        }
    }
}
