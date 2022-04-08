using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCApp.Migrations
{
    public partial class AlterAmigosSeedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Amigos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Elon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Amigos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Pepe");
        }
    }
}
