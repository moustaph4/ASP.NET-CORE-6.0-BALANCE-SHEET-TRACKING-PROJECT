using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilancoTakipProjesi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoris",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    KategoriTipi = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoris", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Islems",
                columns: table => new
                {
                    IslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    Tutar = table.Column<int>(type: "int", nullable: false),
                    Not = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islems", x => x.IslemId);
                    table.ForeignKey(
                        name: "FK_Islems_Kategoris_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoris",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Islems_KategoriId",
                table: "Islems",
                column: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Islems");

            migrationBuilder.DropTable(
                name: "Kategoris");
        }
    }
}
