using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arvuti_MVC.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TellimusModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arvuti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monitor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Klaviatuur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hiir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lisainfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AruvtiOlemas = table.Column<bool>(type: "bit", nullable: false),
                    MonitorOlemas = table.Column<bool>(type: "bit", nullable: false),
                    KlaviatuurOlemas = table.Column<bool>(type: "bit", nullable: false),
                    HiirOlemas = table.Column<bool>(type: "bit", nullable: false),
                    Pakitud = table.Column<bool>(type: "bit", nullable: false),
                    ValjaSaadetud = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TellimusModel", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TellimusModel");
        }
    }
}
