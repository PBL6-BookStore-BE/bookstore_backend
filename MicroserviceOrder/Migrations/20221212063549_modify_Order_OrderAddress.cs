using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceOrder.Migrations
{
    public partial class modify_Order_OrderAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderAddress",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderAddress",
                table: "Orders");
        }
    }
}
