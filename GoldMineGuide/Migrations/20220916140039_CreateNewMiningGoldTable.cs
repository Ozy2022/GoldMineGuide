using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoldMineGuide.Migrations
{
    public partial class CreateNewMiningGoldTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flower",
                columns: table => new
                {
                    Mining_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mining_Place = table.Column<string>(nullable: true),
                    Method_Name = table.Column<string>(nullable: true),
                    Process_Type = table.Column<string>(nullable: true),
                    Business_Type = table.Column<string>(nullable: true),
                    Mining_Produced_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flower", x => x.Mining_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flower");
        }
    }
}
