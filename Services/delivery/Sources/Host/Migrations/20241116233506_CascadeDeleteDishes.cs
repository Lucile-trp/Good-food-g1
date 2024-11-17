using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Host.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dish_order_order_id",
                table: "dish");

            migrationBuilder.AddForeignKey(
                name: "FK_dish_order_order_id",
                table: "dish",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dish_order_order_id",
                table: "dish");

            migrationBuilder.AddForeignKey(
                name: "FK_dish_order_order_id",
                table: "dish",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id");
        }
    }
}
