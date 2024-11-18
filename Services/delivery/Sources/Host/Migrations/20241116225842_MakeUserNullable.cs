using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Host.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delivery_address_user_customer_id",
                table: "delivery_address");

            migrationBuilder.DropForeignKey(
                name: "FK_order_user_customer_id",
                table: "order");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                table: "order",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                table: "delivery_address",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "dish",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    cost = table.Column<double>(type: "double precision", nullable: false),
                    restaurant_id = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dish", x => x.id);
                    table.ForeignKey(
                        name: "FK_dish_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_dish_order_id",
                table: "dish",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_address_user_customer_id",
                table: "delivery_address",
                column: "customer_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_user_customer_id",
                table: "order",
                column: "customer_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delivery_address_user_customer_id",
                table: "delivery_address");

            migrationBuilder.DropForeignKey(
                name: "FK_order_user_customer_id",
                table: "order");

            migrationBuilder.DropTable(
                name: "dish");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                table: "delivery_address",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_address_user_customer_id",
                table: "delivery_address",
                column: "customer_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_user_customer_id",
                table: "order",
                column: "customer_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
