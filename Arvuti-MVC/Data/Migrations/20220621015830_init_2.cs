using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arvuti_MVC.Data.Migrations
{
    public partial class init_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TellimusModel",
                table: "TellimusModel");

            migrationBuilder.RenameTable(
                name: "TellimusModel",
                newName: "Tellimused");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tellimused",
                table: "Tellimused",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tellimused",
                table: "Tellimused");

            migrationBuilder.RenameTable(
                name: "Tellimused",
                newName: "TellimusModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TellimusModel",
                table: "TellimusModel",
                column: "ID");
        }
    }
}
