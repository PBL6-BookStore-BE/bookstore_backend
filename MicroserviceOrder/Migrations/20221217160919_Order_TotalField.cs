using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceOrder.Migrations
{
    public partial class Order_TotalField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Total",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");
        }
    }
}
