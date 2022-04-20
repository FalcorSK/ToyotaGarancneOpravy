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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarancnaOprava");
        }
    }
}
