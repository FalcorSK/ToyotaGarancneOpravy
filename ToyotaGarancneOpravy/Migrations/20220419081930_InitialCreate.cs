using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToyotaGarancneOpravy.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarancnaOprava",
                columns: table => new
                {
                    GarancnaOpravaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZakazkaTb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TbScan = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TbFileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ZakazkaTg = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TgScan = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TgFileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Vin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cws = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Protokol = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarancnaOprava", x => x.GarancnaOpravaId);
                });

            migrationBuilder.CreateTable(
                name: "PrilohaTyp",
                columns: table => new
                {
                    PrilohaTypId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrilohaNazov = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrilohaPopis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Aktivna = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateddBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrilohaTyp", x => x.PrilohaTypId);
                });

            migrationBuilder.CreateTable(
                name: "Priloha",
                columns: table => new
                {
                    PrilohaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarancnaOpravaId = table.Column<int>(type: "int", nullable: false),
                    PrilohaTyp = table.Column<int>(type: "int", nullable: true),
                    PrilohaNazov = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrilohaSubor = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PrilohaFileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrilohaTypNavigationPrilohaTypId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priloha", x => x.PrilohaId);
                    table.ForeignKey(
                        name: "FK_Priloha_GarancnaOprava_GarancnaOpravaId",
                        column: x => x.GarancnaOpravaId,
                        principalTable: "GarancnaOprava",
                        principalColumn: "GarancnaOpravaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Priloha_PrilohaTyp_PrilohaTypNavigationPrilohaTypId",
                        column: x => x.PrilohaTypNavigationPrilohaTypId,
                        principalTable: "PrilohaTyp",
                        principalColumn: "PrilohaTypId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Priloha_GarancnaOpravaId",
                table: "Priloha",
                column: "GarancnaOpravaId");

            migrationBuilder.CreateIndex(
                name: "IX_Priloha_PrilohaTypNavigationPrilohaTypId",
                table: "Priloha",
                column: "PrilohaTypNavigationPrilohaTypId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Priloha");

            migrationBuilder.DropTable(
                name: "GarancnaOprava");

            migrationBuilder.DropTable(
                name: "PrilohaTyp");
        }
    }
}
